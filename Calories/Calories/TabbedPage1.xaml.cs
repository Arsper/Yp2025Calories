using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ChekEmail cE = new ChekEmail();
            await Navigation.PushAsync(cE);
            NavigationPage.SetHasNavigationBar(cE, false);
        }
    }
}