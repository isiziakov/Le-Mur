using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using le_mur.View.Auth;
using le_mur.Helpers;

namespace le_mur
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new StartPage());
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
