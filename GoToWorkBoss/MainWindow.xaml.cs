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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int _bossId { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miWorker_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Workers>();
            window._bossId = _bossId;
            window.Show();
        }

        private void miShift_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Shifts>();
            window.Show();
        }

        private void miMachine_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Machines>();
            window.Show();
        }

        private void miGetList_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowCertificateReport>();
            window.Show();
        }

        private void miGetReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowReportProductShift>();
            window._bossId = _bossId;
            window.Show();
        }

        private void miGetGraph_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Statistic>();
            window.Show();
        }
    }
}
