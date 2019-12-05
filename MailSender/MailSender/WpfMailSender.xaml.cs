using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public static List<string> listStrMails;
        public static List<string> mails;
        string senderMail;
        string smtp;
        int port;
        public WpfMailSender()
        {
            InitializeComponent();
        }
        private void BtnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            senderMail = txbEmail.Text + cbMail.SelectedItem;
            string strPassword = passwordBox.Password;  // для WinForms - string strPassword = passwordBox.Text;
            foreach (string mail in listStrMails)
            {
                // Используем using, чтобы гарантированно удалить объект MailMessage после использования
                using (MailMessage mm = new MailMessage(senderMail, mail))
                {
                    // Формируем письмо
                    mm.Subject = txbTopic.Text; // Заголовок письма
                    mm.Body = txbMessage.Text;       // Тело письма
                    mm.IsBodyHtml = false;           // Не используем html в теле письма
                                                     // Авторизуемся на smtp-сервере и отправляем письмо
                                                     // Оператор using гарантирует вызов метода Dispose, даже если при вызове 
                                                     // методов в объекте происходит исключение.
                    switch (cbMail.SelectedIndex)
                    {
                        case 0:
                            smtp = "smtp.mail.ru";
                            port = 25;
                            break;
                        case 1:
                            smtp = "smtp.yandex.ru";
                            port = 25;
                            break;
                        case 2:
                            smtp = "smtp.gmail.com";
                            port = 58;
                            break;
                    }

                    using (SmtpClient sc = new SmtpClient(smtp, port))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(senderMail, strPassword);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                        }
                    }
                } //using (MailMessage mm = new MailMessage("sender@yandex.ru", mail))
            }
            MessageBox.Show("Работа завершена.");
        }
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            listStrMails.Add(txbRecip.Text);
            listBoxRecip.Items.Add(txbRecip.Text);
            txbRecip.Text = "";
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            listStrMails.RemoveAt(listBoxRecip.SelectedIndex);
            listBoxRecip.Items.Remove(listBoxRecip.SelectedItem);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listStrMails = new List<string>();
            mails = new List<string> { "@mail.ru", "@yandex.ru", "@gmail.com" };
            foreach (var mail in mails)
            {
                cbMail.Items.Add(mail);
            }
        }
    }
}
