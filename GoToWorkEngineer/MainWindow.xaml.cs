using System.Windows;
using Unity;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int _engineerId { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miParts_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Parts>();
            window.Show();
        }

        private void miProducts_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Products>();
            window.Show();
        }

        private void miCertificates_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Certificates>();
            window._engineerId = _engineerId;
            window.Show();
        }

        private void miShiftReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowShiftReport>();
            window.Show();
        }

        private void miCertificateShiftReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowCertificateShiftReport>();
            window._engineerId = _engineerId;
            window.Show();
        }

        private void miGraph_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Statistic>();
            window.Show();
        }

        private void miAccount_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Engineer>();
            window.Id = _engineerId;
            window.Show();
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
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
    }
}
