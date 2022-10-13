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
    /// Логика взаимодействия для Machine.xaml
    /// </summary>
    public partial class Machine : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        MachineLogic _logic;
        public int _bossId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int, (string, int)> machineWorkers;
        private Dictionary<int, (string, int)> machineParts;

        public Machine(MachineLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void LoadData()
        {
            try
            {
                if (machineWorkers != null)
                {
                    List<MachineWorkerViewModel> list = new List<MachineWorkerViewModel>();
                    foreach (var worker in machineWorkers)
                    {
                        list.Add(new MachineWorkerViewModel { Id = worker.Key, WorkerName = worker.Value.Item1, WorkerCount = worker.Value.Item2 });
                    }
                    dgWorkers.ItemsSource = list;
                    dgWorkers.Columns[0].Visibility = Visibility.Hidden;
                }
                if (machineParts != null)
                {
                    List<MachinePartViewModel> list = new List<MachinePartViewModel>();
                    foreach (var part in machineParts)
                    {
                        list.Add(new MachinePartViewModel { Id = part.Key, PartName = part.Value.Item1, PartCount = part.Value.Item2 });
                    }
                    dgParts.ItemsSource = list;
                    dgParts.Columns[0].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    MachineViewModel view = _logic.Read(new MachineBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tbName.Text = view.Name.ToString();
                        tbGuarantee.Text = view.Guarantee.ToString();
                        machineWorkers = view.MachineWorkers;
                        machineParts = view.MachineParts;
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
                machineWorkers = new Dictionary<int, (string, int)>();
                machineParts = new Dictionary<int, (string, int)>();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbGuarantee.Text))
            {
                MessageBox.Show("Заполните гарантию станка", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new MachineBindingModel
                {
                    Id = id,
                    Name = tbName.Text,
                    Guarantee = Convert.ToInt32(tbGuarantee.Text),
                    MachineParts = machineParts,
                    MachineWorkers = machineWorkers
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddWorker>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!machineWorkers.ContainsKey(window.Id))
                {
                    machineWorkers.Add(window.Id, (window.WorkerName, window.WorkerCount));
                }

            }
            LoadData();
        }

        private void DeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            if (dgWorkers.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MachineWorkerViewModel worker = (MachineWorkerViewModel)dgWorkers.SelectedCells[0].Item;
                    try
                    {
                        machineWorkers.Remove(worker.Id);
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

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddPart>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!machineParts.ContainsKey(window.Id))
                {
                    machineParts.Add(window.Id, (window.PartName, window.PartCount));
                }

            }
            LoadData();
        }

        private void DeletePart_Click(object sender, RoutedEventArgs e)
        {
            if (dgParts.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MachinePartViewModel part = (MachinePartViewModel)dgParts.SelectedCells[0].Item;
                    try
                    {
                        machineParts.Remove(part.Id);
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
