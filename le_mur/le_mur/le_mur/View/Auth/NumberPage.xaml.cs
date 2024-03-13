using le_mur.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace le_mur.View.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberPage : ContentPage
    {
        public NumberPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new NumberViewModel() { Navigation = this.Navigation };
        }
    }
}