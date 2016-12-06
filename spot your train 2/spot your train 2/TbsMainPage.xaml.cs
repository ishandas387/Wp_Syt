using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Net.NetworkInformation;
using spot_your_train_2.Models;
using Newtonsoft.Json;

namespace spot_your_train_2
{
    public partial class TbsMainPage : PhoneApplicationPage
    {
        public TbsMainPage()
        {
            InitializeComponent();

            
        }

        private void fromchanged(object sender, RoutedEventArgs e)
        {


            if (checkNetworkConnection())
            {
                WebClient w = new WebClient();

                //this.IsBusy = true;
                w.DownloadStringCompleted += whome_DownloadStringCompleted;
                w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/suggest_station/name/" + fromauto.Text + "/apikey/thnxp9240/"));


            }
        }


        private void whome_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Error == null)
            {
                Sname sm = null;
                sm = JsonConvert.DeserializeObject<Sname>(e.Result);
                List<string> st = new List<string>();
                for (int i = 0; i < sm.total;i++ )
                {

                    st.Add(sm.station[i].fullname + "-" + sm.station[i].code);
                }


                    fromauto.ItemsSource = st;
                //MessageBox.Show(r.main.ToString());
            }
            fromauto.PopulateComplete();
        }

        public static bool checkNetworkConnection()
        {

            var ni = NetworkInterface.NetworkInterfaceType;

            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }

        private void topop(object sender, PopulatingEventArgs e)
        {

            if (checkNetworkConnection())
            {
                WebClient w = new WebClient();

                //this.IsBusy = true;
                w.DownloadStringCompleted += w_DownloadStringCompleted;
                w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/suggest_station/name/" + toauto.Text + "/apikey/thnxp9240/"));


            }

        }

        private void w_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Sname sm = null;
                sm = JsonConvert.DeserializeObject<Sname>(e.Result);
                List<string> st = new List<string>();
                for (int i = 0; i < sm.total; i++)
                {

                    st.Add(sm.station[i].fullname + "-" + sm.station[i].code);
                }


                toauto.ItemsSource = st;
                //MessageBox.Show(r.main.ToString());
            }
            toauto.PopulateComplete();
        }

        private void bsearch_Click(object sender, RoutedEventArgs e)
        {
            if(fromauto.Text=="")
            {

                MessageBox.Show("Please enter station name/code");
               
            }
            else
            {
                if (toauto.Text == "")
                {
                    MessageBox.Show("Please enter station name/code");



                }
                else
                {
                    bool b = checkNetwork();
                    if(!b)
                    {
                        MessageBox.Show("Seems like the device is not connected to internet!");
                    }
                    else
                    {
                        string d, m;
                        d = dp.Value.Value.Day.ToString();
                        if (d.Length == 1)
                        {
                            d = "0" + d;
                        }
                        m = dp.Value.Value.Month.ToString();
                        if (m.Length == 1)
                        {
                            m = "0" + m;
                        }



                        string lnk = "/TbsResultPage.xaml?parameter=";
                        lnk = lnk + fromauto.Text + "&parameter1=";
                        lnk = lnk + toauto.Text + "&parameter2=";
                        lnk = lnk + d + "-" + m;


                        NavigationService.Navigate(new Uri((lnk), UriKind.Relative));


                    }
                    
                    
                    
                }
            }

        }
        public static bool checkNetwork()
        {

            var ni = NetworkInterface.NetworkInterfaceType;

            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

            fromauto.Text = "";
            toauto.Text = "";

        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            if(fromauto.Text!="" ||toauto.Text!="")
            {
                string temp;
                temp = fromauto.Text;
                fromauto.Text = toauto.Text;
                toauto.Text = temp;

            }

        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            string s = "/MainPage.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));


        }

    }

}