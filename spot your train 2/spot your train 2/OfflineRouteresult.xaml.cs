using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
using Windows.Storage.Streams;
using spot_your_train_2.Model;
using Newtonsoft.Json;
using spot_your_train_2.Model.RouteModel;

namespace spot_your_train_2
{
    public partial class OfflineRouteresult : PhoneApplicationPage
    {
        public OfflineRouteresult()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string fname = NavigationContext.QueryString["parameter"];

            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("routefolder");
            StorageFile textFile = await folder.GetFileAsync(fname+".txt");
            using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
            {

                using (DataReader textReader = new DataReader(textStream))
                {
                    uint textLength = (uint)textStream.Size;
                    await textReader.LoadAsync(textLength);
                    string jsonContents = textReader.ReadString(textLength);
                    RouteMain result = null;
                    //savethisroute = e.Result;
                    result = JsonConvert.DeserializeObject<RouteMain>(jsonContents);
                    if (result.response_code == 200)
                    {


                        oroutelistresult.ItemsSource = result.route;
                        tnum.Text = result.train.number;
                        tname.Text = result.train.name;
                        osun.Text = result.train.days[0].runs;
                        omon.Text = result.train.days[1].runs;
                        otue.Text = result.train.days[2].runs;
                        owed.Text = result.train.days[3].runs;
                        othu.Text = result.train.days[4].runs;
                        ofri.Text = result.train.days[5].runs;
                        osat.Text = result.train.days[6].runs;

                        opb1.Visibility = Visibility.Collapsed;
                        //a1.Visibility = Visibility.Collapsed;
                        //a2.Visibility = Visibility.Collapsed;



                    }

                }
            }


        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            string s = "/MainPage.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));

        }

    }
}