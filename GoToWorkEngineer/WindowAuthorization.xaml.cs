using GoToWorkBusinessLogic.BusinessLogics;
using System;
using System.Windows;
using Unity;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для WelcomeWindow.xaml
    /// </summary>
    public partial class WindowAuthorization : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly EngineerLogic logic;

        public WindowAuthorization(EngineerLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка",
               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Password))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                int engineerId = logic.CheckPassword(tbLogin.Text, tbPassword.Password);
                var window = Container.Resolve<MainWindow>();
                window._engineerId = engineerId;
                window.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Register>();
            window.Show();
            Close();
        }

    }
}
