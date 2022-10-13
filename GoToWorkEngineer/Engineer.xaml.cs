using System;
using System.Windows;
using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.BindingModels;
using Unity;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для Engineer.xaml
    /// </summary>
    public partial class Engineer : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        EngineerLogic _logic;
        public int Id { set { id = value; } }
        private int? id;
        public Engineer(EngineerLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка",
               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Выберите пароль", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Выберите логин", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new EngineerBindingModel
                {
                    Id = id,
                    FIO = tbName.Text,
                    Password = tbPassword.Text,
                    Login = tbLogin.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowAuthorization>();
            MessageBoxResult result = MessageBox.Show("Выйти из учетной записи?", "Вопрос", MessageBoxButton.YesNo,
              MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                window.Show();
                Close();
            }
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = _logic.Read(new EngineerBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        tbName.Text = view.FIO;
                        tbPassword.Text = view.Password;
                        tbLogin.Text = view.Login;
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
