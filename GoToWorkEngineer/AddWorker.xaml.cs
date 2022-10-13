using System;
using System.Windows;
using GoToWorkContracts.ViewModels;
using GoToWorkBusinessLogic.BusinessLogics;
using Unity;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        WorkerLogic _logic;
        private WorkerViewModel workerViewModel;
        public int Id
        {
            get
            {
                return workerViewModel.Id;
            }
            set
            {
                cbWorkerName.SelectedItem = value;
            }
        }
        public string WorkerName { get { return cbWorkerName.Text; } }

        public int WorkerCount
        {
            get { return Convert.ToInt32(tbCount.Text); }
            set
            {
                tbCount.Text = value.ToString();
            }
        }

        public AddWorker(WorkerLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbWorkerName.SelectedValue == null)
                {
                    MessageBox.Show("Выберите работника", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                if (tbCount.Text == null)
                {
                    MessageBox.Show("Введите количество работников", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                workerViewModel = (WorkerViewModel)cbWorkerName.SelectedValue;
                DialogResult = true;
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

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            var list = _logic.Read(null);
            if (list.Count > 0)
            {
                try
                {
                    cbWorkerName.DisplayMemberPath = "Name";
                    cbWorkerName.ItemsSource = list;
                    cbWorkerName.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
        }
    }
}
