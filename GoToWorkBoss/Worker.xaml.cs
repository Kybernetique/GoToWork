using GoToWorkContracts.BindingModels;
using GoToWorkBusinessLogic.BusinessLogics;
using System;
using System.Windows;
using Unity;

namespace GoToWorkBoss
{
    /// <summary>
    /// Логика взаимодействия для Worker.xaml
    /// </summary>
    public partial class Worker : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        WorkerLogic _logic;
        public int _bossId { get; set; }
        public int Id { set { id = value; } }
        private int? id;

        public Worker(WorkerLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)

            {
                try
                {
                    var view = _logic.Read(new WorkerBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        tbName.Text = view.Name;
                        tbHourSalary.Text = view.HourSalary.ToString();
                        tbPosition.Text = view.Position;
                    }
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
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Заполните имя", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(tbHourSalary.Text))
            {
                MessageBox.Show("Заполните зарплату за час", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(tbPosition.Text))
            {
                MessageBox.Show("Заполните должность", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new WorkerBindingModel
                {
                    Id = id,
                    Name = tbName.Text,
                    HourSalary = Convert.ToDecimal(tbHourSalary.Text),
                    Position = tbPosition.Text,
                    BossId = _bossId

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

    }
}
