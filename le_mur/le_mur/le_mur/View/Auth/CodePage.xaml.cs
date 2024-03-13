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
    public partial class CodePage : ContentPage
    {
        public CodePage(string number)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new CodeViewModel(number) { Navigation = this.Navigation };

            entry0.NextView = entry1;
            entry0.PreviousView = null;

            entry1.NextView = entry2;
            entry1.PreviousView = entry0;

            entry2.NextView = entry3;
            entry2.PreviousView = entry1;

            entry3.NextView = entry4;
            entry3.PreviousView = entry2;

            entry4.NextView = null;
            entry4.PreviousView = entry3;
        }
    }
}