using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calories
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            TabbedPage1 main = new TabbedPage1();
            NavigationPage.SetHasNavigationBar(main, false);
            MainPage = new NavigationPage(main);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
