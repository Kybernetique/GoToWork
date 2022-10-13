using GoToWorkContracts.BindingModels;
using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.ViewModels;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        CertificateLogic _logic;

        private List<StatisticByCertificateViewModel> _certificates = new List<StatisticByCertificateViewModel>();

        public Statistic(CertificateLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void LoadData()
        {
            _certificates.Clear();
            List<CertificateViewModel> certificates = _logic.Read(new CertificateBindingModel { DateTo = dpTo.SelectedDate, DateFrom = dpFrom.SelectedDate });
            foreach (var certificate in certificates)
            {
                _certificates.Add(new StatisticByCertificateViewModel { CertificateName = certificate.Name, ProductCount = certificate.CertificateProducts.Count });
            }
            DataGridView.ItemsSource = _certificates;
            DataGridView.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BuildGraph_Click(object sender, RoutedEventArgs e)
        {
            if (dpFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату начала",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpTo.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpFrom.SelectedDate >= dpTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadData();
            Build(_certificates);
        }

        private void Build(List<StatisticByCertificateViewModel> statistic)
        {
            SeriesCollection series = new SeriesCollection();
            List<string> certificateName = new List<string>();
            ChartValues<int> productCount = new ChartValues<int>();

            foreach (var item in statistic)
            {
                certificateName.Add(item.CertificateName);
                productCount.Add(Convert.ToInt32(item.ProductCount));
            }

            Graph.AxisX.Clear();
            Graph.AxisX.Add(new Axis()

            {
                Title = "\nАкт",
                Labels = certificateName
            });

            LineSeries productLine = new LineSeries
            {
                Title = "Кол-во изделий: ",
                Values = productCount
            };

            series.Add(productLine);
            Graph.Series = series;
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
