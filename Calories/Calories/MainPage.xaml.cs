using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calories
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, EventArgs e) {
            if (UserNameEntry.Text == "") { 
            }
        }
        private async void  ButtonForgotPassword(object sender, EventArgs e)
        {
            //ListViewPage1 pg1 = new ListViewPage1();
            //await Navigation.PushAsync(pg1);
            //NavigationPage.SetHasNavigationBar(pg1, false);
        }
        private async void ButtonSignUp(object sender, EventArgs e)
        {
            SignUp pg1 = new SignUp();
            await Navigation.PushAsync(pg1);
            NavigationPage.SetHasNavigationBar(pg1, false);
        }
    }
}