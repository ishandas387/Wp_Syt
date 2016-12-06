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
using spot_your_train_2.Models;
using Windows.Storage;
using Windows.Storage.Streams;

namespace spot_your_train_2
{
    public partial class TbsResultPage : PhoneApplicationPage
    {
        int i = 1;

        string savethisshit;
        public TbsResultPage()
        {
            InitializeComponent();



        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string source = NavigationContext.QueryString["parameter"];
            string dest = NavigationContext.QueryString["parameter1"];
            from.Text = source;
            to.Text = dest;

            string[] fr = source.Split('-');
            string[] tt = dest.Split('-');

            string date = NavigationContext.QueryString["parameter2"];

            WebClient w = new WebClient();

            //this.IsBusy = true;
            w.DownloadStringCompleted += tbs_DownloadStringCompleted;
            w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/between/source/" + fr[1] + "/dest/" + tt[1] + "/date/" + date + "/apikey/thnxp9240/"));


        }

        private void tbs_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            TbsMain tb = null;
            savethisshit = e.Result;
            tb = JsonConvert.DeserializeObject<TbsMain>(e.Result);
            if (tb.response_code == 200)
            {
                if (tb.total != 0)
                {
                    tbslist.ItemsSource = tb.train;
                    pbtbs.Visibility = Visibility.Collapsed;
                    ad1.Visibility = Visibility.Collapsed;
                    ad2.Visibility = Visibility.Collapsed;



                }
                else
                {
                    MessageBox.Show("We do not have this info currently! Please try with some other date..");
                    pbtbs.Visibility = Visibility.Collapsed;
                }





            }
        }




        private void tbslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (tbslist.SelectedItem != null)
            {


                Train myItem = ((Train)(sender as LongListSelector).SelectedItem);




                MessageBoxResult result =
    MessageBox.Show("Would you like to see the Route?",
    "Route", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    // MessageBox.Show(myItem.number);
                    tbslist.SelectedItem = null;
                    string s = "/Routeresult.xaml?parameter=" + myItem.number;
                    NavigationService.Navigate(new Uri((s), UriKind.Relative));
                }




            }



        }

       

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("tbsfolder");
            StorageFile textFile = await folder.GetFileAsync("1.txt");
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
                            tbslist.ItemsSource = tb.train;
                            pbtbs.Visibility = Visibility.Collapsed;
                            ad1.Visibility = Visibility.Collapsed;
                            ad2.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private async void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            if (savethisshit != null)
            {
                string jsonContents = savethisshit;

                i++;
                string sr = from.Text;
                string dt = to.Text;
                string fname = sr + " to " + dt;
                StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("tbsfolder", CreationCollisionOption.OpenIfExists);

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
    }
}