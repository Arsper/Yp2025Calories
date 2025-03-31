using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calories
{
    public static class WorkWithEmail
    {
        private static int _verificationCode;
        private static EventHandler _currentHandler;

        public static async Task EmailWorkAsync(Entry timeEntry, Button button, string email)
        {
            try
            {
                // Запускаем таймер и отправку письма параллельно
                var timerTask = RunTimerAsync(timeEntry, button, email);
                var sendEmailTask = SendVerificationEmailAsync(email);

                await Task.WhenAll(timerTask, sendEmailTask);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке email: {ex.Message}");
            }
        }

        private static async Task RunTimerAsync(Entry timeEntry, Button button, string email)
        {
            if (_currentHandler != null)
            {
                button.Clicked -= _currentHandler;
            }

            await Device.InvokeOnMainThreadAsync(() =>
            {
                button.BackgroundColor = Color.Transparent;
                timeEntry.Text = "30";
            });

            for (int i = 30; i >= 0; i--)
            {
                await Task.Delay(1000);
                await Device.InvokeOnMainThreadAsync(() => timeEntry.Text = i.ToString());
            }

            await Device.InvokeOnMainThreadAsync(() =>
            {
                button.BackgroundColor = Color.White;

                // Назначаем новый обработчик
                _currentHandler = async (sender, e) => await EmailWorkAsync(timeEntry, button, email);
                button.Clicked += _currentHandler;
            });
        }

        private static async Task SendVerificationEmailAsync(string email)
        {
            await Task.Run(() =>
            {
                var r = new Random();
                _verificationCode = r.Next(100000, 999999);

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("arseniyzyk81@gmail.com", "Калории");
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Подтверждение почты";
                    message.Body = $"Введите код чтобы подтвердить вашу почту: {_verificationCode}";

                    using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(
                            "arseniyzyk81@gmail.com",
                            "kiow hswo nqyv nrue");

                        smtp.Send(message);
                    }
                }
            });
        }

        public static bool ValidateCode(string userInput)
        {
            return int.TryParse(userInput, out int code) && code == _verificationCode;
        }
    }
}