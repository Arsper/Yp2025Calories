using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calories
{
    class WorkWithEmail
    {
        static private int rInt;
        static string _email;
        static Entry _timeEntry;
        static private EventHandler _currentHandler;

        static public async void TimerEmail(Entry timeEntry, Button button,string email)
        {
            _email=email;
            _timeEntry=timeEntry;
            await MessangInPos(email);
            if (_currentHandler != null)
            {
                button.Clicked -= _currentHandler;
            }

            button.BackgroundColor = Color.Transparent;

            timeEntry.Text = "30";
            for (int i = 30; i >= 0; i--)
            {
                await Task.Delay(1000);
                timeEntry.Text = i.ToString();
            }

            button.BackgroundColor = Color.White;

            // Назначаем новый обработчик
            _currentHandler = (sender, e) => OnNewCodeClicked(sender, e, button);
            button.Clicked += _currentHandler;
        }
        static public Task MessangInPos(string email)
        {
            System.Random r = new System.Random();
            rInt = r.Next(100000, 999999);
            MailAddress fromMailAddress = new MailAddress("arseniyzyk81@gmail.com", "Калории");
            MailAddress toAddress = new MailAddress(email, "Пользователь");

            MailMessage message = new MailMessage(fromMailAddress, toAddress);

            message.Subject = "Подтверждение почты";
            message.Body = "Введите код чтобы подтвердить вашу почту: " + rInt;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, "kiow hswo nqyv nrue");

            smtpClient.Send(message);
            return Task.FromResult(0);
        }
        private async static void OnNewCodeClicked(object sender, EventArgs e, Button button)
        {
            TimerEmail(_timeEntry, button, _email);
        }
    }
}