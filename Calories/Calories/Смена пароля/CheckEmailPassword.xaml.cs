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
	public partial class CheckEmailPassword : ContentPage
	{
		public CheckEmailPassword ()
		{
			InitializeComponent ();
		}
        private async void ChekCode(object sender, EventArgs e)
        {
            InputNewPassword iNewPas = new InputNewPassword();
            await Navigation.PushAsync(iNewPas);
            NavigationPage.SetHasNavigationBar(iNewPas, false);
        }
        private async void newCode(object sender, EventArgs e)
        {

        }
    }
}