using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using le_mur.ViewModel;
using le_mur.Model;

namespace le_mur.View
{
    public partial class ChannelPage : ContentPage
    {
        public ChannelPage(ChatInfo chat)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new ChannelViewModel(chat) { Navigation = this.Navigation };
        }
    }
}
