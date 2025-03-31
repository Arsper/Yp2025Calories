using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChekEmail : ContentPage
    {
        private string _email;
        private readonly string _login;
        private readonly string _password;

        public ChekEmail()
        {
            InitializeComponent();

            // Запускаем асинхронную инициализацию после загрузки страницы
            LoadEmailAndStartTimer();
        }

        private async void LoadEmailAndStartTimer()
        {
            try
            {
                // Получаем email из SecureStorage
                _email = await SecureStorage.GetAsync("reg_email");

                if (string.IsNullOrEmpty(_email))
                {
                    await DisplayAlert("Ошибка", "Email не найден", "OK");
                    return;
                }

                // Запускаем таймер и отправку кода
                await WorkWithEmail.EmailWorkAsync(TimeEmail, ButtonNewEmail, _email);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось загрузить email: {ex.Message}", "OK");
            }
        }

        private async void Chekcode(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EnterEmailCode.Text))
            {
                await DisplayAlert("Ошибка", "Введите код подтверждения", "OK");
                return;
            }

            if (WorkWithEmail.ValidateCode(EnterEmailCode.Text))
            {
                var uI = new UsersInfo();
                await Navigation.PushAsync(uI);
                NavigationPage.SetHasNavigationBar(uI, false);
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный код подтверждения", "OK");
            }
        }

        private async void NewCode_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_email))
            {
                await DisplayAlert("Ошибка", "Email не загружен", "OK");
                return;
            }

            await WorkWithEmail.EmailWorkAsync(TimeEmail, ButtonNewEmail, _email);
        }
    }
}