using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для Certificate.xaml
    /// </summary>
    public partial class Certificate : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        CertificateLogic _logic;
        ProductLogic _logicP;
        public int _engineerId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int, string> сertificateProducts;

        public Certificate(CertificateLogic logic, ProductLogic logicP)
        {
            InitializeComponent();
            _logic = logic;
            _logicP = logicP;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CertificateViewModel view = _logic.Read(new CertificateBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tbName.Text = view.Name;
                        tbCost.Text = view.Cost.ToString();
                        dpDate.SelectedDate = view.Date;
                        сertificateProducts = view.CertificateProducts;
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
                сertificateProducts = new Dictionary<int, string>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (сertificateProducts != null)
                {
                    List<CertificateProductViewModel> list = new List<CertificateProductViewModel>();
                    foreach (var certificate in сertificateProducts)
                    {
                        list.Add(new CertificateProductViewModel { Id = certificate.Key, ProductName = certificate.Value });
                    }
                    DataGridView.ItemsSource = list;
                    DataGridView.Columns[0].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new CertificateBindingModel
                {
                    Id = id,
                    Name = tbName.Text,
                    Date = dpDate.SelectedDate.Value,
                    Cost = Convert.ToDecimal(tbCost.Text),
                    CertificateProducts = сertificateProducts,
                    EngineerId = _engineerId
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

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddProduct>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!сertificateProducts.ContainsKey(window.Id))
                {
                    сertificateProducts.Add(window.Id, window.ProductName);
                }

            }
            LoadData();
            CalculateCost();
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridView.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CertificateProductViewModel product = (CertificateProductViewModel)DataGridView.SelectedCells[0].Item;
                    try
                    {
                        сertificateProducts.Remove(product.Id);
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

                foreach (var cp in сertificateProducts)
                {
                    totalCost += (int)_logicP.Read(new ProductBindingModel { Id = cp.Key })?[0].Cost;
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
