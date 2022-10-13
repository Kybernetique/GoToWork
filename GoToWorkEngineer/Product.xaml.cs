using GoToWorkContracts.BindingModels;
using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.ViewModels;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;
using System.Collections.Generic;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        ProductLogic _logic;
        WorkerLogic _logicW;
        PartLogic _logicP;
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int, (string, int)> productWorkers;
        private Dictionary<int, (string, int)> productParts;

        public Product(ProductLogic logic, WorkerLogic logicW, PartLogic logicP)
        {
            InitializeComponent();
            _logic = logic;
            _logicW = logicW;
            _logicP = logicP;
        }

        private void LoadData()
        {
            try
            {
                if (productWorkers != null)
                {
                    List<ProductWorkerViewModel> list = new List<ProductWorkerViewModel>();
                    foreach (var worker in productWorkers)
                    {
                        list.Add(new ProductWorkerViewModel { Id = worker.Key, WorkerName = worker.Value.Item1, WorkerCount = worker.Value.Item2 });
                    }
                    DataGridWorkers.ItemsSource = list;
                    DataGridWorkers.Columns[0].Visibility = Visibility.Hidden;
                }
                if (productParts != null)
                {
                    List<ProductPartViewModel> list = new List<ProductPartViewModel>();
                    foreach (var part in productParts)
                    {
                        list.Add(new ProductPartViewModel { Id = part.Key, PartName = part.Value.Item1, PartCount = part.Value.Item2 });
                    }
                    DataGridParts.ItemsSource = list;
                    DataGridParts.Columns[0].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ProductViewModel view = _logic.Read(new ProductBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tbName.Text = view.Name;
                        tbCost.Text = view.Cost.ToString();
                        productWorkers = view.ProductWorkers;
                        productParts = view.ProductParts;
                        LoadData();
                        CalculateCost();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
            else
            {
                productWorkers = new Dictionary<int, (string, int)>();
                productParts = new Dictionary<int, (string, int)>();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbCost.Text))
            {
                MessageBox.Show("Заполните стоимость", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new ProductBindingModel
                {
                    Id = id,
                    Name = tbName.Text,
                    Cost = Convert.ToInt32(tbCost.Text),
                    ProductParts = productParts,
                    ProductWorkers = productWorkers
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddWorker>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!productWorkers.ContainsKey(window.Id))
                {
                    productWorkers.Add(window.Id, (window.WorkerName, window.WorkerCount));
                }
                else
                {
                    productWorkers[window.Id] = (window.WorkerName, window.WorkerCount);
                }

            }
            LoadData();
            CalculateCost();
        }

        private void btnDeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridWorkers.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ProductWorkerViewModel worker = (ProductWorkerViewModel)DataGridWorkers.SelectedCells[0].Item;
                    try
                    {
                        productWorkers.Remove(worker.Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                    CalculateCost();
                }
            }
        }

        private void btnAddPart_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddPart>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!productParts.ContainsKey(window.Id))
                {
                    productParts.Add(window.Id, (window.PartName, window.PartCount));
                }
                else
                {
                    productParts[window.Id] = (window.PartName, window.PartCount);
                }

            }
            LoadData();
            CalculateCost();
        }

        private void btnDeletePart_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridParts.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ProductPartViewModel product = (ProductPartViewModel)DataGridParts.SelectedCells[0].Item;
                    try
                    {
                        productParts.Remove(product.Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                    CalculateCost();
                }
            }
        }

        private void CalculateCost()
        {
            try
            {
                int totalCost = 0;
                foreach (var pw in productWorkers)
                {
                    totalCost += pw.Value.Item2 * (int)_logicW.Read(new WorkerBindingModel { Id = pw.Key })?[0].HourSalary;
                }
                foreach (var pp in productParts)
                {
                    totalCost += pp.Value.Item2 * (int)_logicP.Read(new PartBindingModel { Id = pp.Key })?[0].Cost;
                }
                tbCost.Text = totalCost.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
        }

        public static string GetPropertyDisplayName(object descriptor)
        {
            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                DisplayNameAttribute displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }
            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }
            return null;
        }
    }
}
