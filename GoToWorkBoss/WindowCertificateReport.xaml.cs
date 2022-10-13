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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace GoToWorkBoss
{
    /// <summary>
    /// Логика взаимодействия для WindowCertificateReport.xaml
    /// </summary>
    public partial class WindowCertificateReport : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        WorkerLogic logicW;
        CertificateReportLogic logicC;
        List<WorkerViewModel> list = new List<WorkerViewModel>();
        public WindowCertificateReport(CertificateReportLogic _logicC, WorkerLogic _logicW)
        {
            InitializeComponent();
            logicW = _logicW;
            logicC = _logicC;
        }

        private void LoadData()
        {
            try
            {
                if (list != null)
                {
                    DataGridView.ItemsSource = list;
                    DataGridView.Columns[0].Visibility = Visibility.Hidden;
                    DataGridView.Columns[4].Visibility = Visibility.Hidden;
                    DataGridView.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddWorker>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!list.Contains(logicW.Read(new WorkerBindingModel { Id = window.Id })[0]))
                {
                    list.Add(logicW.Read(new WorkerBindingModel { Id = window.Id })[0]);
                    LoadData();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridView.SelectedIndex != -1)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    WorkerViewModel worker = (WorkerViewModel)DataGridView.SelectedCells[0].Item;
                    try
                    {
                        list.Remove(worker);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void SaveToWord_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        logicC.SaveToWordFile(dialog.FileName, list);
                        System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                }
            }
        }

        private void SaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        logicC.SaveToExcelFile(dialog.FileName, list);
                        System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
