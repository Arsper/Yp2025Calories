using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Calories.ChekData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Calories.Class;

namespace Calories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        UserRegistrationService service;
        public TabbedPage1()
        {
            InitializeComponent();
            service = new UserRegistrationService("http://c33661db.beget.tech/api/ChekUser.php");
        }
        private async void ButtonSignUp(object sender, EventArgs e)
        {
            SignUp pg1 = new SignUp();
            await Navigation.PushAsync(pg1);
            NavigationPage.SetHasNavigationBar(pg1, false);
        }
        private async void ButtonForgotPassword(object sender, EventArgs e)
        {
            InputEnaill Ip = new InputEnaill();
            await Navigation.PushAsync(Ip);
            NavigationPage.SetHasNavigationBar(Ip, false);
        }
        private async void ButtonClick(object sender, EventArgs e)
        {
            if (CheckUserDataInputs())
            {
                try
                {
                    var result = await service.CheckUserExistsAsync(NewUserNameEntry.Text, NewEmailEntry.Text);

                    if (result.Status == "success")
                    {
                        await DisplayAlert("Супер", $"Пользователь может войти: {result.Message}", "ОК");
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", $"Пользователь не может войти: {result.Message}", "ОК");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "ОК");
                }
                //await SecureStorage.SetAsync("reg_email", NewEmailEntry.Text);
                //await SecureStorage.SetAsync("reg_login", NewUserNameEntry.Text);
                //await SecureStorage.SetAsync("reg_password", NewPasswordEntry.Text);
                //ChekEmail cE = new ChekEmail();
                //await Navigation.PushAsync(cE);
                //NavigationPage.SetHasNavigationBar(cE, false);
            }
            
        }
        
        private  bool CheckUserDataInputs()
        {
            if(!CheckLogin(NewUserNameEntry.Text) || !CheckEmail(NewEmailEntry.Text) || !CheckPassword(NewPasswordEntry.Text) || !CheckTwoPassword(NewPasswordEntry.Text, NewPasswordChekEntry.Text))
            {
                return false;
            }
            return true;
        }
    }
}