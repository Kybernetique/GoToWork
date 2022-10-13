using System;
using System.Windows;
using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.BindingModels;
using Unity;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для Part.xaml
    /// </summary>
    public partial class Part : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        PartLogic _logic;
        public int Id { set { id = value; } }
        private int? id;
        public Part(PartLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(tbPrice.Text))
            {
                MessageBox.Show("Заполните стоимость", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new PartBindingModel
                {
                    Id = id,
                    Name = tbName.Text,
                    Cost = Convert.ToDecimal(tbPrice.Text)
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
            this.DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = _logic.Read(new PartBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        tbName.Text = view.Name;
                        tbPrice.Text = view.Cost.ToString();
                    }
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
