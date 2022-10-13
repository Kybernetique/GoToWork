using GoToWorkBusinessLogic.BusinessLogics;
using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using BoldReports.UI.Xaml;
using BoldReports.Windows;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using Unity;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Forms;

namespace GoToWorkEngineer
{
    /// <summary>
    /// Логика взаимодействия для WindowCertificateShiftReport.xaml
    /// </summary>
    public partial class WindowCertificateShiftReport : Window
    {
        public new IUnityContainer Container { get; set; }

        ReportCertificateShiftLogic _logic;

        public int _engineerId { get; set; }

        public WindowCertificateShiftReport(ReportCertificateShiftLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            reportViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"C:/Users/Tony/Desktop/GoToWork/GoToWorkEngineer/Report.rdlc");
            reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (dpFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату начала",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpTo.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpFrom.SelectedDate >= dpTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var dataSource = _logic.GetCertificateShift(new ReportCertificateShiftBindingModel
                {
                    EngineerId = _engineerId,
                    DateFrom = dpFrom.SelectedDate,
                    DateTo = dpTo.SelectedDate
                });
                ReportDataSource source = new ReportDataSource("DataSetReport", dataSource);
                reportViewer.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        [Obsolete]
        private void btnMail_Click(object sender, RoutedEventArgs e)
        {
            {
                if (dpFrom.SelectedDate >= dpTo.SelectedDate)
                {
                    MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MailMessage msg = new MailMessage();
                SmtpClient client = new SmtpClient();
                try
                {
                    using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
                    {
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string basis = "Отчет по заказам пользователей в период";
                            msg.Subject = basis;

                            msg.From = new MailAddress("shadaevLab7@gmail.com");
                            msg.To.Add(tbEmailAddress.Text);
                            msg.IsBodyHtml = true;

                            _logic.SaveToPdfFile(new ReportCertificateShiftBindingModel
                            {
                                EngineerId = _engineerId,
                                FileName = dialog.FileName,
                                DateFrom = dpFrom.SelectedDate,
                                DateTo = dpTo.SelectedDate
                            });

                            Attachment attach = new Attachment(dialog.FileName, MediaTypeNames.Application.Octet);
                            ContentDisposition disposition = attach.ContentDisposition;

                            disposition.CreationDate = System.IO.File.GetCreationTime(dialog.FileName);
                            disposition.ModificationDate = System.IO.File.GetLastWriteTime(dialog.FileName);
                            disposition.ReadDate = System.IO.File.GetLastAccessTime(dialog.FileName);

                            msg.Attachments.Add(attach);
                            client.Host = "smtp.gmail.com";
                            NetworkCredential basicauthenticationinfo = new NetworkCredential("shadaevLab7@gmail.com", "upypplpibvzmpxml");
                            client.Port = int.Parse("587");
                            client.EnableSsl = true;
                            client.UseDefaultCredentials = false;
                            client.Credentials = basicauthenticationinfo;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.Send(msg);

                            MessageBox.Show($"Сообщение отправлено на почту - {tbEmailAddress.Text}!", "Успех", MessageBoxButton.OK,
                             MessageBoxImage.Information);
                        }
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
