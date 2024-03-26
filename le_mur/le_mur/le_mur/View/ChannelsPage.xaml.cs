using le_mur.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace le_mur.View
{
    public partial class ChannelsPage : ContentPage
    {
        public ChannelsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new ChannelsViewModel() { Navigation = this.Navigation };
        }
    }
}