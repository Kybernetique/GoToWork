using GoToWorkContracts.BindingModels;
using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace GoToWorkBoss
{
    /// <summary>
    /// Логика взаимодействия для Shift.xaml
    /// </summary>
    public partial class Shift : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        ShiftLogic _logic;
        WorkerLogic workerLogic;
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int, (string, int)> shiftWorker;

        public Shift(ShiftLogic logic, WorkerLogic workerLogic)
        {
            InitializeComponent();
            _logic = logic;
            this.workerLogic = workerLogic;
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ShiftViewModel view = _logic.Read(new ShiftBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tbTimeDay.Text = view.DayTime;
                        tbDate.SelectedDate = view.Date;
                        shiftWorker = view.ShiftWorkers;
                        LoadData();
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
                shiftWorker = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (shiftWorker != null)
                {
                    List<ShiftWorkerViewModel> list = new List<ShiftWorkerViewModel>();
                    foreach (var worker in shiftWorker)
                    {
                        list.Add(new ShiftWorkerViewModel { Id = worker.Key, WorkerName = worker.Value.Item1});
                    }
                    dgShiftWorker.ItemsSource = list;
                    dgShiftWorker.Columns[0].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (tbDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new ShiftBindingModel
                {
                    Id = id,
                    Date = tbDate.SelectedDate.Value,
                    DayTime = tbTimeDay.Text,
                    ShiftWorkers = shiftWorker
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

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddWorker>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!shiftWorker.ContainsKey(window.Id))
                {
                    shiftWorker.Add(window.Id, (window.WorkerName, window.WorkerCount));
                }

            }
            LoadData();
        }


        private void btnDeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            if (dgShiftWorker.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ShiftWorkerViewModel worker = (ShiftWorkerViewModel)dgShiftWorker.SelectedCells[0].Item;
                    try
                    {
                        shiftWorker.Remove(worker.Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
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
