using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Calories.ChekData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            InitializeComponent();
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
                ChekEmail cE = new ChekEmail();
                await Navigation.PushAsync(cE);
                NavigationPage.SetHasNavigationBar(cE, false);
            }
            
        }
        
        private bool CheckUserDataInputs()
        {
            if(!CheckLogin(NewUserNameEntry.Text) || !CheckEmail(NewEmailEntry.Text) || !CheckPassword(NewPasswordEntry.Text) || CheckTwoPassword(NewPasswordEntry.Text, NewPasswordChekEntry.Text))
            {
                return false;
            }
            return true;
        }
    }
}