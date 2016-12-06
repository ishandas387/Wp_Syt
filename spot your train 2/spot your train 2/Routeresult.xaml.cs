﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Newtonsoft.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using spot_your_train_2.Model.RouteModel;

namespace spot_your_train_2
{
    public partial class Routeresult : PhoneApplicationPage
    {
        string savethisroute;
        public Routeresult()
        {
          
            InitializeComponent();
           

            //  string r = "{ \"position\": \"Currently Saraighat Express is Destination Reached and there is No Delay when it arrived at Guwahati (GHY) at 09:30 AM (On Time)\", \"error\": \"\", \"train_number\": \"12345\", \"response_code\": 200, \"route\": [ { \"actdep\": \"03:50 PM\", \"station\": \"Howrah Jn (HWH)\", \"actarr\": \"Source\", \"status\": \"No Delay\", \"schdep\": \"03:50 PM\", \"no\": 1, \"scharr\": \"Source\" }, { \"actdep\": \"05:20 PM\", \"station\": \"Barddhaman Jn (BWN)\", \"actarr\": \"05:17 PM\", \"status\": \"13 Mins Late\", \"schdep\": \"05:06 PM\", \"no\": 2, \"scharr\": \"05:04 PM\" }, { \"actdep\": \"06:21 PM\", \"station\": \"Bolpur S Niktn (BHP)\", \"actarr\": \"06:16 PM\", \"status\": \"23 Mins Late\", \"schdep\": \"05:58 PM\", \"no\": 3, \"scharr\": \"05:53 PM\" }, { \"actdep\": \"07:20 PM\", \"station\": \"Rampur Hat (RPH)\", \"actarr\": \"07:18 PM\", \"status\": \"21 Mins Late\", \"schdep\": \"06:59 PM\", \"no\": 4, \"scharr\": \"06:57 PM\" }, { \"actdep\": \"08:59 PM\", \"station\": \"New Farakka Jn (NFK)\", \"actarr\": \"08:57 PM\", \"status\": \"07 Mins Late\", \"schdep\": \"08:52 PM\", \"no\": 5, \"scharr\": \"08:50 PM\" }, { \"actdep\": \"09:45 PM\", \"station\": \"Malda Town (MLDT)\", \"actarr\": \"09:35 PM\", \"status\": \"05 Mins Late\", \"schdep\": \"09:45 PM\", \"no\": 6, \"scharr\": \"09:30 PM\" }, { \"actdep\": \"12:28 AM\", \"station\": \"Kishanganj (KNE)\", \"actarr\": \"12:26 AM\", \"status\": \"48 Mins Late\", \"schdep\": \"11:40 PM\", \"no\": 7, \"scharr\": \"11:38 PM\" }, { \"actdep\": \"02:05 AM\", \"station\": \"New Jalpaiguri (NJP)\", \"actarr\": \"01:40 AM\", \"status\": \"5 Mins. Before\", \"schdep\": \"02:05 AM\", \"no\": 8, \"scharr\": \"01:45 AM\" }, { \"actdep\": \"04:17 AM\", \"station\": \"New Cooch Behar (NCB)\", \"actarr\": \"04:13 AM\", \"status\": \"2 Mins. Before\", \"schdep\": \"04:17 AM\", \"no\": 9, \"scharr\": \"04:15 AM\" }, { \"actdep\": \"04:38 AM\", \"station\": \"New Alipurduar (NOQ)\", \"actarr\": \"04:36 AM\", \"status\": \"01 Min Late\", \"schdep\": \"04:37 AM\", \"no\": 10, \"scharr\": \"04:35 AM\" }, { \"actdep\": \"06:27 AM\", \"station\": \"New Bongaigaon (NBQ)\", \"actarr\": \"06:20 AM\", \"status\": \"5 Mins. Before\", \"schdep\": \"06:27 AM\", \"no\": 11, \"scharr\": \"06:25 AM\" }, { \"actdep\": \"09:13 AM\", \"station\": \"Kamakhya (KYQ)\", \"actarr\": \"09:00 AM\", \"status\": \"11 Mins. Before\", \"schdep\": \"09:13 AM\", \"no\": 12, \"scharr\": \"09:11 AM\" }, { \"actdep\": \"Destination\", \"station\": \"Guwahati (GHY)\", \"actarr\": \"09:30 AM\", \"status\": \"No Delay\", \"schdep\": \"Destination\", \"no\": 13, \"scharr\": \"09:30 AM\" } ], \"total\": 13 }";


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string p = NavigationContext.QueryString["parameter"];

            WebClient w = new WebClient();

            //this.IsBusy = true;
            w.DownloadStringCompleted += whome_DownloadStringCompleted;
            w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/route/train/"+p+"/apikey/thnxp9240/"));


        }

        private void whome_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //  throw new NotImplementedException();
            if (e.Error == null)
            {

                RouteMain result = null;
                savethisroute = e.Result;
                result = JsonConvert.DeserializeObject<RouteMain>(e.Result);
                if (result.response_code == 200)
                {


                    routelistresult.ItemsSource = result.route;
                    tnum.Text = result.train.number;
                    tname.Text = result.train.name;
                    sun.Text = result.train.days[0].runs;
                    mon.Text = result.train.days[1].runs;
                    tue.Text = result.train.days[2].runs;
                    wed.Text = result.train.days[3].runs;
                    thu.Text = result.train.days[4].runs;
                    fri.Text = result.train.days[5].runs;
                    sat.Text = result.train.days[6].runs;
                    
                    pb1.Visibility = Visibility.Collapsed;
                    a1.Visibility = Visibility.Collapsed;
                    a2.Visibility = Visibility.Collapsed;
                    


                }



            }
        }

        private async void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

            if (savethisroute != null)
            {
                string jsonContents = savethisroute;
                    
                //i++;

                string num = tnum.Text;
                string nam = tname.Text;
                string fname = num + " - " + nam;
                StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("routefolder", CreationCollisionOption.OpenIfExists);

                //StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile textFile = await folder.CreateFileAsync(fname + ".txt", CreationCollisionOption.ReplaceExisting);
                using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
                {

                    using (DataWriter textWriter = new DataWriter(textStream))
                    {
                        textWriter.WriteString(jsonContents);
                        await textWriter.StoreAsync();
                    }
                }
                MessageBox.Show("Saved!!");



            }

        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            string s = "/MainPage.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));


        }
    }
}