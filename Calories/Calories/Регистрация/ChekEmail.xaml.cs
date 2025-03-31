using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Calories.WorkWithEmail;
using System.Data.Common;

namespace Calories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChekEmail : ContentPage
    {
        string email;
        string login;
        string password;
        public ChekEmail(string Email, string Login, string Password)
        {
            InitializeComponent();
            email = Email;
            login = Login;
            password = Password;
            //MessangInPos(email);
            TimerEmail(TimeEmail, ButtonNewEmail, email);
            //tonNewEmail);
        }

        

        private async void Chekcode(object sender, EventArgs e)
        {
            UsersInfo uI = new UsersInfo();
            await Navigation.PushAsync(uI);
            NavigationPage.SetHasNavigationBar(uI, false);
        }
        private async void newCode(object sender, EventArgs e)
        {

        }
    }
}