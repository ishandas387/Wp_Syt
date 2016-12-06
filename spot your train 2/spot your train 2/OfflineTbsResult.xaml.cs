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
using Newtonsoft.Json;
using spot_your_train_2.Models;

namespace spot_your_train_2
{
    public partial class OfflineTbsResult : PhoneApplicationPage
    {
        public OfflineTbsResult()
        {
            InitializeComponent();
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string fname = NavigationContext.QueryString["parameter"];

            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("tbsfolder");
            StorageFile textFile = await folder.GetFileAsync(fname+".txt");
            using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
            {

                using (DataReader textReader = new DataReader(textStream))
                {
                    uint textLength = (uint)textStream.Size;
                    await textReader.LoadAsync(textLength);
                    string jsonContents = textReader.ReadString(textLength);
                    TbsMain tb = null;

                    tb = JsonConvert.DeserializeObject<TbsMain>(jsonContents);
                    if (tb.response_code == 200)
                    {
                        if (tb.total != 0)
                        {
                            otbslist.ItemsSource = tb.train;
                            ofrom.Text = tb.train[0].from.name;
                            oto.Text = tb.train[0].to.name;
                            opbtbs.Visibility = Visibility.Collapsed;
                            //ad1.Visibility = Visibility.Collapsed;
                            //ad2.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }


        }

        private void otbslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (otbslist.SelectedItem != null)
            {


                Train myItem = ((Train)(sender as LongListSelector).SelectedItem);




                MessageBoxResult result =
    MessageBox.Show("Would you like to see the Route?",
    "Route", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    // MessageBox.Show(myItem.number);
                    otbslist.SelectedItem = null;
                    string s = "/Routeresult.xaml?parameter=" + myItem.number;
                    NavigationService.Navigate(new Uri((s), UriKind.Relative));
                }




            }


        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            string s = "/MainPage.xaml" ;
            NavigationService.Navigate(new Uri((s), UriKind.Relative));
        }


    }
}