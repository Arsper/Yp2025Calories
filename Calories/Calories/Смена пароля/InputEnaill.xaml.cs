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
    public partial class InputEnaill : ContentPage
    {
        public InputEnaill()
        {
            InitializeComponent();
        }
        private async void ButtonChekEmail(object sender, EventArgs e)
        {
            CheckEmailPassword CheckEmailPas = new CheckEmailPassword();
            await Navigation.PushAsync(CheckEmailPas);
            NavigationPage.SetHasNavigationBar(CheckEmailPas, false);
        }
    }
}