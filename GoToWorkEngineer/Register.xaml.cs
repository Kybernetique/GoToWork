using GoToWorkContracts.BindingModels;
using GoToWorkBusinessLogic.BusinessLogics;
using System;
using System.Windows;
using Unity;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private readonly EngineerLogic logic;
        public Register(EngineerLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbFIO.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка",
               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new EngineerBindingModel
                {
                    Id = id,
                    FIO = tbFIO.Text,
                    Password = tbPassword.Text,
                    Login = tbLogin.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);

                var window = Container.Resolve<WindowAuthorization>();
                window.Show();
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
            var window = Container.Resolve<WindowAuthorization>();
            window.Show();
            Close();
        }
    }
}
