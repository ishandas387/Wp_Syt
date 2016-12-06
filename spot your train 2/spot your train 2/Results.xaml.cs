using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace spot_your_train_2
{
    public partial class Results : PhoneApplicationPage
    {
        string p;
        public Results()
        {
            InitializeComponent();
        }

        private void onRefresh_Click(object sender, EventArgs e)
        {
            wb1.Navigate(new Uri(wb1.Source.AbsoluteUri));

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            p = NavigationContext.QueryString["parameter"];
            string d = NavigationContext.QueryString["parameter1"];
            string address = "http://m.runningstatus.in/status/" + p + "/" + d;
            wb1.IsScriptEnabled = false;
            

            //wb1.Source = new Uri("http://m.runningstatus.in/status/?parameter=p", UriKind.Absolute);
            wb1.Navigate(new Uri(address));
          
            //pb.Visibility = Visibility.Collapsed;
        }

       
        private void n1(object sender, NavigationEventArgs e)
        {
            pb.Visibility = Visibility.Collapsed;
           
        }

        private void n2(object sender, NavigatingEventArgs e)
        {
            pb.Visibility = Visibility.Visible;
        }
    }
}