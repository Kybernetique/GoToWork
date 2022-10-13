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
    /// Логика взаимодействия для Machines.xaml
    /// </summary>
    public partial class Machines : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        MachineLogic _logic;
        public Machines(MachineLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Machine>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                LoadData();
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                var list = _logic.Read(null);
                if (list != null)
                {
                    DataGridView.ItemsSource = list;
                    DataGridView.Columns[0].Visibility = Visibility.Hidden;
                    DataGridView.Columns[3].Visibility = Visibility.Hidden;
                    DataGridView.Columns[4].Visibility = Visibility.Hidden;
                }
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridView.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MachineViewModel machine = (MachineViewModel)DataGridView.SelectedCells[0].Item;
                    int id = Convert.ToInt32(machine.Id);
                    try
                    {
                        _logic.Delete(new MachineBindingModel { Id = id });
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

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridView.SelectedIndex != -1)
            {
                var window = Container.Resolve<Machine>();
                MachineViewModel machine = (MachineViewModel)DataGridView.SelectedCells[0].Item;
                window.Id = Convert.ToInt32(machine.Id);
                window.ShowDialog();
                if (window.DialogResult == true)
                {
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
