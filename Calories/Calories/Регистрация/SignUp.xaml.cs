using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
        }
        private async void ButtonSignIn(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void ButtonClick(object sender, EventArgs e)
        {
            ChekEmail cE = new ChekEmail();
            await Navigation.PushAsync(cE);
            NavigationPage.SetHasNavigationBar(cE, false);
        }
    }

}