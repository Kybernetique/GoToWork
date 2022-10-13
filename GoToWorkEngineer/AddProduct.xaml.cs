using System;
using System.Windows;
using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.ViewModels;
using Unity;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        ProductLogic _logic;
        private ProductViewModel productViewModel;
        public int Id
        {
            get
            {
                return productViewModel.Id;
            }
            set
            {
                cbProductName.SelectedItem = value;
            }
        }

        public string ProductName { get { return cbProductName.Text; } }

        public AddProduct(ProductLogic logic)
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
                    cbProductName.DisplayMemberPath = "Name";
                    cbProductName.ItemsSource = list;
                    cbProductName.SelectedItem = null;
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
                if (cbProductName.SelectedValue == null)
                {
                    MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                productViewModel = (ProductViewModel)cbProductName.SelectedValue;
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
