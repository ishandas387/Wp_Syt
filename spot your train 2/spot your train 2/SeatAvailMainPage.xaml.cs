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
using spot_your_train_2.Model;
using Newtonsoft.Json;

namespace spot_your_train_2
{
    public partial class SeatAvailMainPage : PhoneApplicationPage
    {
        string[] cl = { "CC", "SL", "1A", "2A", "3A","2S" };
        string[] gn = { "GN","CK","FT","DP", "LD","PQ","HP", "HO", "DF", "3A","RE","GNRS","RC","RS","YU","LB" };
        public SeatAvailMainPage()
        {
            InitializeComponent();
            listpickclass.ItemsSource = cl;
            listpickerqut.ItemsSource = gn;

        }

     
        private void whome_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                spot_your_train_2.Model.RootObject ro = null;
                ro = JsonConvert.DeserializeObject<spot_your_train_2.Model.RootObject>(e.Result);
                Trainname.ItemsSource = ro.train;
                //MessageBox.Show(r.main.ToString());
            }
            Trainname.PopulateComplete();
        }

        private void autofrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void from_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
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


                autofrom.ItemsSource = st;
                //MessageBox.Show(r.main.ToString());
            }
            autofrom.PopulateComplete();
        }

        private void autoto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void to_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
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


                autoto.ItemsSource = st;
                //MessageBox.Show(r.main.ToString());
            }
            autoto.PopulateComplete();
            
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

        private void Trainname_Populating(object sender, PopulatingEventArgs e)
        {
             if (checkNetwork())
            {
                WebClient w = new WebClient();

                //this.IsBusy = true;
                w.DownloadStringCompleted += whome_DownloadStringCompleted;
                w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/suggest_train/trains/" + Trainname.Text + "/apikey/thnxp9240/"));


            }

        }

        private void autofrom_Populating(object sender, PopulatingEventArgs e)
        {
            if (checkNetwork())
            {
                WebClient w = new WebClient();

                //this.IsBusy = true;
                w.DownloadStringCompleted += from_DownloadStringCompleted;
                w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/suggest_station/name/" + autofrom.Text + "/apikey/thnxp9240/"));


            }

        }

        private void autoto_Populating(object sender, PopulatingEventArgs e)
        {

            if (checkNetwork())
            {
                WebClient w = new WebClient();

                //this.IsBusy = true;
                w.DownloadStringCompleted += to_DownloadStringCompleted;
                w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/suggest_station/name/" + autoto.Text + "/apikey/thnxp9240/"));


            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

            string s = "/MainPage.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Trainname.Text=="" || autofrom.Text=="" || autoto.Text=="")
            {
                MessageBox.Show("Please enter the details!");
            }
            else
            {
                if(chknetwrk())
                {
                    string d, m;
                    d = dpsa.Value.Value.Day.ToString();
                    if (d.Length == 1)
                    {
                        d = "0" + d;
                    }
                    m = dpsa.Value.Value.Month.ToString();
                    if (m.Length == 1)
                    {
                        m = "0" + m;
                    }
                    bool flag;

                    string trn = Trainname.Text;
                    string [] trnarr;
                    if(Char.IsLetter(trn[0]))
                    {
                        trnarr = trn.Split('(');
                        trnarr[1] = trnarr[1].Replace(")", "");
                        flag = true; 
                    }
                    else
                    {
                        trnarr = trn.Split(' ');
                        flag = false;
                    }

                    if (flag)
                    {
                        trn = trnarr[1];

                    }
                    else
                    {

                        trn = trnarr[0];

                    }


                    string lnk = "/SeatAvailResult.xaml?parameter=";
                    lnk = lnk + trn + "&parameter1=";
                    lnk = lnk + autofrom.Text + "&parameter2=";
                    lnk = lnk + autoto.Text + "&parameter3=";
                    lnk = lnk + d + "-" + m+"-"+dpsa.Value.Value.Year +"&parameter4=";
                    lnk = lnk + listpickclass.SelectedItem.ToString()+"-"+listpickerqut.SelectedItem.ToString();


                    NavigationService.Navigate(new Uri((lnk), UriKind.Relative));

                }
                else
                {
                    MessageBox.Show("Phone is not connected to internet!");
                }
            }

        }
        public static bool chknetwrk()
        {

            var ni = NetworkInterface.NetworkInterfaceType;

            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            if (autofrom.Text != "" || autoto.Text != "")
            {
                string temp;
                temp = autofrom.Text;
                autofrom.Text = autofrom.Text;
                autofrom.Text = temp;

            }

        }

    }
}