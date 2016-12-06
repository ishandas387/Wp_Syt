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

namespace spot_your_train_2
{
    public partial class Savedrts : PhoneApplicationPage
    {
        public Savedrts()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            List<string> l = new List<string>();
            StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("routefolder", CreationCollisionOption.OpenIfExists);

           // StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("routefolder");
            var files = await folder.GetFilesAsync();
            if(files.Count==0)
            {
                MessageBox.Show("You have not saved anything yet...");

            }
            else
            {
                foreach (var file in files)
                {
                    string f = file.Name.Replace(".txt", "");
                    l.Add(f);

                }
                ortslist.ItemsSource = l;

            }
           

           
               

            
           
        }
        

        private void ortslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ortslist.SelectedItem != null)
            {


                var myItem = ((sender as LongListSelector).SelectedItem);




                MessageBoxResult result =
    MessageBox.Show("Would you like to see the route?",
    "Route", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    // MessageBox.Show(myItem.number);
                    ortslist.SelectedItem = null;
                    string s = "/OfflineRouteresult.xaml?parameter=" + myItem;
                    NavigationService.Navigate(new Uri((s), UriKind.Relative));
                }




            }

        }


        private async void  Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as MenuItem).DataContext;
            var folder1 = await ApplicationData.Current.LocalFolder.GetFolderAsync("routeFolder");

            var file1 = await folder1.GetFileAsync(item+".txt");
            await file1.DeleteAsync(StorageDeleteOption.PermanentDelete);

            ortslist.ItemsSource.Remove(item);
            MessageBox.Show("Deleted");
            List<string> l = new List<string>();
            StorageFolder folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("routefolder", CreationCollisionOption.OpenIfExists);

            // StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("routefolder");
            var files = await folder.GetFilesAsync();
            if (files.Count == 0)
            {
                MessageBox.Show("You have not saved anything yet...");

            }
            else
            {
                foreach (var file in files)
                {
                    string f = file.Name.Replace(".txt", "");
                    l.Add(f);

                }
                ortslist.ItemsSource = l;

            }

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

            string s = "/MainPage.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));


        }
    }
}