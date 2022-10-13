using System;
using System.Windows;
using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.ViewModels;
using Unity;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для AddPart.xaml
    /// </summary>
    public partial class AddPart : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        PartLogic _logic;
        private PartViewModel partViewModel;
        public int Id
        {
            get
            {
                return partViewModel.Id;
            }
            set
            {
                cbPartName.SelectedItem = value;
            }
        }
        public string PartName { get { return cbPartName.Text; } }

        public int PartCount
        {
            get { return Convert.ToInt32(tbCount.Text); }
            set
            {
                tbCount.Text = value.ToString();
            }
        }

        public AddPart(PartLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = _logic.Read(null);
            if (list.Count > 0)
            {
                try
                {
                    cbPartName.DisplayMemberPath = "Name";
                    cbPartName.ItemsSource = list;
                    cbPartName.SelectedItem = null;
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
                if (cbPartName.SelectedValue == null)
                {
                    MessageBox.Show("Выберите деталь", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                if (tbCount.Text == null)
                {
                    MessageBox.Show("Введите количество деталей", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                partViewModel = (PartViewModel) cbPartName.SelectedValue;
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
    }
}
