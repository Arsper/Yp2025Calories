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
    public partial class ChekEmail : ContentPage
    {
        public ChekEmail()
        {
            InitializeComponent();
        }
        private async void ChekCode(object sender, EventArgs e)
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