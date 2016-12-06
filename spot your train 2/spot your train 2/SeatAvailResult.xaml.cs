using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using spot_your_train_2.Model.pnr;


namespace spot_your_train_2
{
    public partial class SeatAvailResult : PhoneApplicationPage
    {
        public SeatAvailResult()
        {
            InitializeComponent();

            //string j = "{ \"availability\": [ { \"status\": \"AVAILABLE 396\", \"date\": \"15-9-2015\" }, { \"status\": \"AVAILABLE 443\", \"date\": \"16-9-2015\" }, { \"status\": \"AVAILABLE 432\", \"date\": \"17-9-2015\" }, { \"status\": \"AVAILABLE 452\", \"date\": \"18-9-2015\" }, { \"status\": \"AVAILABLE 347\", \"date\": \"19-9-2015\" }, { \"status\": \"AVAILABLE 10\", \"date\": \"20-9-2015\" } ], \"train_name\": \"NDLS SHATABDI E\", \"quota\": { \"quota_code\": \"GN\", \"quota_name\": \"GENERAL QUOTA\" }, \"error\": false, \"train_number\": \"12001\", \"to\": { \"code\": \"NDLS\", \"lng\": 44.42233, \"name\": \"NEW DELHI\", \"lat\": 33.3569963 }, \"class\": { \"class_name\": \"AC CHAIR CAR\", \"class_code\": \"CC\" }, \"from\": { \"code\": \"BPL\", \"lng\": 77.412615, \"name\": \"BHOPAL JN\", \"lat\": 23.2599333 }, \"response_code\": 200 }";

           
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string tnum = NavigationContext.QueryString["parameter"];
            string f = NavigationContext.QueryString["parameter1"];
            string t = NavigationContext.QueryString["parameter2"];
            string date = NavigationContext.QueryString["parameter3"];
            string cq = NavigationContext.QueryString["parameter4"];
            string [] c  = cq.Split('-');
            string[] fr = f.Split('-');
            string[] tt = t.Split('-');
            WebClient w = new WebClient();

            //this.IsBusy = true;
            w.DownloadStringCompleted += sa_DownloadStringCompleted;
            w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/check_seat/train/"+tnum+"/source/"+fr[1]+"/dest/"+tt[1]+"/date/"+date+"/class/"+c[0]+"/quota/"+c[1]+"/apikey/thnxp9240/"));


        }

        private void sa_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            SeatAvail sa = JsonConvert.DeserializeObject<SeatAvail>(e.Result);
            if (sa.response_code == 200)
            {
                if(sa.error ==true)
                {
                    MessageBox.Show("Try changing the source/destination");

                }
                else
                {
                    availlist.ItemsSource = sa.availability;
                    cls.Text = sa.@class.class_name + " - " + sa.@class.class_code;
                    qut.Text = sa.quota.quota_name + " - " + sa.quota.quota_code;
                    f.Text = sa.from.name + "-" + sa.from.code;
                    t.Text = sa.to.name + "-" + sa.to.code;
                    tnum.Text = sa.train_number;
                    tname.Text = sa.train_name;
                    pbsa.Visibility = Visibility.Collapsed;
                    ad1sa.Visibility = Visibility.Collapsed;

                    ad2sa.Visibility = Visibility.Collapsed;
                }
              

            }
                     
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            string s = "/MainPage.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));
        }

        private void availlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (availlist.SelectedItem != null)
            {


                Availability myItem = ((Availability)(sender as LongListSelector).SelectedItem);




                MessageBoxResult result =
    MessageBox.Show("Would you like to see the fares?",
    "Fares", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {

                    // MessageBox.Show(myItem.number);
                    availlist.SelectedItem = null;
                    string s = "/Faredetails.xaml?parameter=" + myItem.date;
                    s = s + "&parameter1=" + tnum.Text + "&parameter2=" + f.Text.Split('-')[1] + "&parameter3=" + t.Text.Split('-')[1];
                    //string c = cls.Text.Replace(" ", "");
                    string q = qut.Text.Replace(" ", "");
                    s = s +"&parameter4=" + q.Split('-')[1];
                    NavigationService.Navigate(new Uri((s), UriKind.Relative));
                }




            }



        }
    }
}