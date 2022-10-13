using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int WorkerCount { get { return Convert.ToInt32(tbWorkerCount.Text); } }

        public AddWorker(WorkerLogic logic)
        {
            InitializeComponent();
            _logic = logic;
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
                workerViewModel = (WorkerViewModel)cbWorkerName.SelectionBoxItem;
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

    }
}
