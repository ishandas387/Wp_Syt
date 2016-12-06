using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using spot_your_train_2.FareModel;
using Newtonsoft.Json;

namespace spot_your_train_2
{
    public partial class Faredetails : PhoneApplicationPage
    {
        public Faredetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string doj = NavigationContext.QueryString["parameter"];
            string tn = NavigationContext.QueryString["parameter1"];
            string f = NavigationContext.QueryString["parameter2"];
            string t = NavigationContext.QueryString["parameter3"];
            //string c = NavigationContext.QueryString["parameter4"];
            string q = NavigationContext.QueryString["parameter4"];

            
            WebClient w = new WebClient();

            //this.IsBusy = true;
            w.DownloadStringCompleted += fd_DownloadStringCompleted;
            w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/fare/train/"+tn+"/source/"+f+"/dest/"+t+"/age/25/quota/"+q+"/doj/"+doj+"/apikey/thnxp9240/"));


        }

        private void fd_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
             Fr fd = JsonConvert.DeserializeObject<Fr>(e.Result);
             if (fd.response_code == 200)
             {
                 farelist.ItemsSource = fd.fare;
                 tn.Text = fd.train.name;
                 tnu.Text = fd.train.number;
                 qt.Text = fd.quota.name + "-" + fd.quota.name;
                 f.Text = fd.from.name + "-" + fd.from.code;
                 t.Text = fd.to.name + "-" + fd.to.code;
                 pbfd.Visibility = Visibility.Collapsed;
                 fad1.Visibility = Visibility.Collapsed;

             }
            else{
                 MessageBox.Show("Opps! Looks like we don't have the details now.");
             }
        }

      
    }
}