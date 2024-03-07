using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using le_mur.View;

namespace le_mur
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new AuthPage();
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
