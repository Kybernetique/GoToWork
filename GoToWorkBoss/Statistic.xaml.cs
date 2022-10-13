using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using GoToWorkContracts.BindingModels;
using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.ViewModels;

namespace GoToWorkBoss
{
    /// <summary>
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        ShiftLogic _logic;
        WorkerLogic _logicW;
        private List<StatisticByShiftViewModel> _shifts = new List<StatisticByShiftViewModel>();

        public Statistic(ShiftLogic logic, WorkerLogic logicW)
        {
            InitializeComponent();
            _logic = logic;
            _logicW = logicW;
        }
        private void LoadData()
        {
            _shifts.Clear();
            List<ShiftViewModel> shifts = _logic.Read(new ShiftBindingModel { DateTo = dpTo.SelectedDate, DateFrom = dpFrom.SelectedDate });
            foreach (var shift in shifts)
            {
                int salary = 0;
                foreach (var shiftProduct in shift.ShiftWorkers)
                {
                    salary += Convert.ToInt32(_logicW.Read(new WorkerBindingModel { Id = shiftProduct .Key})[0].HourSalary * shiftProduct.Value.Item2);
                }
                _shifts.Add(new StatisticByShiftViewModel { Date = shift.Date.ToShortDateString(), WorkerHourSalary = salary });
            }
            DataGridView.ItemsSource = _shifts;
            DataGridView.Items.Refresh();
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
            Build(_shifts);
        }

        private void Build(List<StatisticByShiftViewModel> statistic)
        {
            SeriesCollection series = new LiveCharts.SeriesCollection();
            List<string> shiftName = new List<string>();
            ChartValues<int> workerSalary = new ChartValues<int>();

            foreach (var item in statistic)
            {
                shiftName.Add(item.Date);
                workerSalary.Add(Convert.ToInt32(item.WorkerHourSalary));
            }

            Graph.AxisX.Clear();
            Graph.AxisX.Add(new Axis()

            {
                Title = "\nДата смены",
                Labels = shiftName
            });

            LineSeries productLine = new LineSeries
            {
                Title = "Зарпалата работников за час: ",
                Values = workerSalary
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
