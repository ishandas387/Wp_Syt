using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using spot_your_train_2.Resources;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Net.NetworkInformation;
using System.IO;
using spot_your_train_2.Model;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace spot_your_train_2
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        int d1, d2, d3, d4;
        int m1, m2, m3;
        
        //int[] day = {};

        int[] year = { DateTime.Now.Year, DateTime.Now.Year-1 };
        string lpday, lpmonth, lpyear;
        // Constructor
        IsolatedStorageSettings aset = IsolatedStorageSettings.ApplicationSettings;


        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            string s = tbTrainnum.Text;

            definedates();
            definemonths();
            if (aset.Contains("tno"))
            {
                //aset.Add("tno", s);
                tbTrainnum.Text = (string)aset["tno"];
            }
            else
            {
                aset.Add("tno", s);
            }
            
            this.lpkyear.ItemsSource = year;
            

            
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void definemonths()
        {
            m1 = DateTime.Now.Month;
            if (m1 == 1)
            {
                m2 = 12;
                m3 = 11;
            }
            else if (m1 == 2)  
            {
                m2 = m1 - 1;
                m3 = 12;

            }
            else{
                m2 = m1 - 1;
                m3 = m2 - 1;


            }

            int[] month = { m1,m2,m3 };
            
             
            this.lpkmonth.ItemsSource = month;

        }

        public void definedates()
        {
            d1 = DateTime.Now.Day;
            if(d1==1)
            {
                m2 = DateTime.Now.Month - 1;
                int temp = DateTime.DaysInMonth(DateTime.Now.Year, m2);
                d2 = temp;
                d3 = d2 - 1;
                d4 = d3 - 1;

            }
            else if(d1==2)
            {
                d2 = d1 - 1;
                m2 = DateTime.Now.Month - 1;
                int temp = DateTime.DaysInMonth(DateTime.Now.Year, m2);
                d3 = temp;
                d4 = d3 - 1;

            }
            else if(d1==3)
            {
                d2 = d1 - 1;
                d3 = d2 - 1;
                m2 = DateTime.Now.Month - 1;
                int temp = DateTime.DaysInMonth(DateTime.Now.Year, m2);
                d4 = temp;
                
            }
            else
            {
                d2 = d1 - 1;
                d3 = d2 - 1;
                d4 = d3 - 1;

            }
           int[] day = { d1,d2,d3,d4 };
           this.lpkDay.ItemsSource = day;

        }

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        

        // Load data for the ViewModel Items
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    if (!App.ViewModel.IsDataLoaded)
        //    {
        //        App.ViewModel.LoadData();
        //    }
        //}

        private void selectionchanged(object sender, SelectionChangedEventArgs e)
        {
            switch((sender as Pivot).SelectedIndex)
            {
                case 0:
                    this.ApplicationBar = this.Resources["firstappbar"] as ApplicationBar;
                    break;

                case 1:
                    this.ApplicationBar = this.Resources["secondappbar"] as ApplicationBar;
                    lListSelector.ItemsSource=LoadFavoirties();
                   /* using (favoriteContext f = new favoriteContext(favoriteContext.ConnectionString))
                    {
                        f.CreateIfNotExists();
                        f.LogDebug = true;
                        lListSelector.ItemsSource = f.Favorites.ToList();
                    }*/
                    break;
                case 2:
                    this.ApplicationBar = this.Resources["firstappbar"] as ApplicationBar;
                    break;

                default:
                    break;

            }
        }
        private List<string> LoadFavoirties()
        {
            List<string> history = new List<string>();
            using (IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (appStorage.FileExists("Browser_Favorties.txt"))
                {
                    using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream("Browser_Favorties.txt", System.IO.FileMode.Open, FileAccess.Read, appStorage)))
                    {
                        var uri = reader.ReadLine();
                        while (!string.IsNullOrEmpty(uri))
                        {
                            history.Add(uri);
                            uri = reader.ReadLine();
                        }
                        reader.Close();
                        return history;
                    }
                }
            }

            return null;
        }
        private void onClear_Click(object sender, EventArgs e)
        {
            tbTrainnum.Text = "";
        }
        private void onRateClick(object sender, EventArgs e)
        {
            MarketplaceReviewTask r = new MarketplaceReviewTask();
            r.Show();
        }
        private void onCheck_Click(object sender, RoutedEventArgs e)
        {
            string s = tbTrainnum.Text;
            aset["tno"] = s;
           // pgconnect.Visibility = Visibility.Visible;
           // tbconnect.Visibility = Visibility.Visible;

            bool b = checkNetworkConnection();

            if (!b)
            {

                MessageBoxResult result = MessageBox.Show("Needs internet connection", "No Connectivity!", MessageBoxButton.OK);
                /*if (result == MessageBoxResult.OK)
                {

                    Application.Current.Terminate();

                }*/
            }
            else
            {
               // pgconnect.Visibility = Visibility.Collapsed;
               // tbconnect.Visibility = Visibility.Collapsed;

                bool flag;
                if (tbTrainnum.Text == "")
                {
                    MessageBox.Show("Please enter the train number");
                }
                else
                {
                    string trn = tbTrainnum.Text;
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

                   // string h = "-";
                    lpday = lpkDay.SelectedItem.ToString();
                    lpmonth = lpkmonth.SelectedItem.ToString();
                    lpyear = DateTime.Now.Year.ToString();
                    if(lpday.Length==1)
                    {
                        lpday = "0" + lpday;

                    }
                    if(lpmonth.Length==1)
                    {
                        lpmonth = "0" + lpmonth;

                    }
                    lpday = lpyear + lpmonth + lpday;
                    string t = null;

                    if(flag)
                    {
                         t = trnarr[1] ;

                    }
                    else{

                        t = trnarr[0];

                    }
                   
                    // string d = lpday + "-" + lpmonth + "-" + lpyear;


                    string first = "/ApiresultStatus.xaml?parameter=";
                    first += t;
                    string second = "&parameter1=";
                    second += lpday;

                    first += second;
                    // MessageBox.Show(first);


                    //string add += first;
                    // add +=second;

                    NavigationService.Navigate(new Uri((first), UriKind.Relative));
                }
            }
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

        private void onFavClick(object sender, EventArgs e)
        {
            
     using (IsolatedStorageFile appStore = IsolatedStorageFile.GetUserStoreForApplication())
        {
            
            StreamWriter sr = new StreamWriter(new IsolatedStorageFileStream("Browser_Favorties.txt", FileMode.Append, appStore));
            sr.WriteLine(tbTrainnum.Text);
            sr.Close();
            MessageBox.Show("Added to Favorites!");
        }
 
        }

        private void onSelected(object sender, SelectionChangedEventArgs e)
        {
            //lListSelector.SelectedItem;
           // var item = sender as LongListSelector;
            tbTrainnum.Text = lListSelector.SelectedItem.ToString();

            syt.SelectedItem = check;
            this.ApplicationBar = this.Resources["firstappbar"] as ApplicationBar;
        }

        private void onDelete(object sender, RoutedEventArgs e)
        {
             List<string> h = new List<string>();
             using (IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication())
             {
                 if (appStorage.FileExists("Browser_Favorties.txt"))
                 {
                     using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream("Browser_Favorties.txt", System.IO.FileMode.Open, FileAccess.ReadWrite, appStorage)))
                     {
                         var uri = reader.ReadLine();
                         while (!string.IsNullOrEmpty(uri))
                         {
                             if (uri == null)
                             {
                                 uri.Replace(lListSelector.SelectedItem.ToString(), "");
                             }

                             h.Add(uri);
                             uri = reader.ReadLine();
                         }
                         reader.Close();
                         lListSelector.ItemsSource = h;
                         //return history;
                     }
                 }
             }
        }
            

        private void clearall(object sender, EventArgs e)
        {
            using (IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (appStorage.FileExists("Browser_Favorties.txt"))
                {
                    appStorage.DeleteFile("Browser_Favorties.txt");
                    lListSelector.ItemsSource = null;
                }
                else
                    MessageBox.Show("You currently don't have anything here!");
            }
        }

        private  void tchanged(object sender, RoutedEventArgs e)
        {

            
            if (checkNetworkConnection())
            {
                WebClient w = new WebClient();

                //this.IsBusy = true;
                w.DownloadStringCompleted += whome_DownloadStringCompleted;
                w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/suggest_train/trains/" + tbTrainnum.Text + "/apikey/thnxp9240/"));
                
               
            }
        }
       
         
        private void whome_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.Error==null)
            {
                RootObject ro = null;
                ro = JsonConvert.DeserializeObject<RootObject>(e.Result);
                tbTrainnum.ItemsSource = ro.train;
                //MessageBox.Show(r.main.ToString());
            }
            tbTrainnum.PopulateComplete();
        }

       

        private void b2click(object sender, RoutedEventArgs e)



        {

            string tn = null;
            bool b = checkNetworkConnection();

            if (!b)
            {

                MessageBoxResult result = MessageBox.Show("Needs internet connection", "No Connectivity!", MessageBoxButton.OK);
                /*if (result == MessageBoxResult.OK)
                {

                    Application.Current.Terminate();

                }*/
            }
            else
            {
                // pgconnect.Visibility = Visibility.Collapsed;
                // tbconnect.Visibility = Visibility.Collapsed;

                bool flag;
                if (tbTrainnum.Text == "")
                {
                    MessageBox.Show("Please enter the train number");
                }
                else
                {
                    string trn = tbTrainnum.Text;
                    string[] trnarr;
                    if (Char.IsLetter(trn[0]))
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

                    // string h = "-";

                    

                    if (flag)
                    {
                        tn = trnarr[1];

                    }
                    else
                    {

                        tn = trnarr[0];

                    }
                }
            }
           
            string s = "/Routeresult.xaml?parameter="+tn;
            NavigationService.Navigate(new Uri((s), UriKind.Relative));
        }

        private void tbsnav(object sender, EventArgs e)
        {
            string s = "/TbsMainPage.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));

        }

        private void savedtbsnav(object sender, EventArgs e)
        {
            string s = "/Savedtrians.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));

        }
        private void savedrtsnav(object sender, EventArgs e)
        {
            string s = "/Savedrts.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));

        }

        private void tbsclick(object sender, EventArgs e)
        {
            string s = "/SeatAvailResult.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
           // pup.IsOpen = true;

            string s = "/SeatAvailMainPage.xaml";
            NavigationService.Navigate(new Uri((s), UriKind.Relative));


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pup.IsOpen = false;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string s = "/PnrResult.xaml?parameter="+pnrnumber.Text;
            NavigationService.Navigate(new Uri((s), UriKind.Relative));


        }

       

    /*    private void hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show(lListSelector.SelectedItem.ToString());
        }*/
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            while (this.NavigationService.BackStack.Any())
            {
                this.NavigationService.RemoveBackEntry();
            }
        }

        private void fromauto_Populating(object sender, PopulatingEventArgs e)
        {
            if (checkNetworkConnection())
            {
                WebClient w = new WebClient();

                //this.IsBusy = true;
                w.DownloadStringCompleted += frmauto_DownloadStringCompleted;
                w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/suggest_station/name/" + fromauto.Text + "/apikey/thnxp9240/"));


            }

        }

        private void frmauto_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
           // throw new NotImplementedException();
            if (e.Error == null)
            {
                spot_your_train_2.Models.Sname sm = null;
                sm = JsonConvert.DeserializeObject<spot_your_train_2.Models.Sname>(e.Result);
                List<string> st = new List<string>();
                for (int i = 0; i < sm.total; i++)
                {

                    st.Add(sm.station[i].fullname + "-" + sm.station[i].code);
                }


                fromauto.ItemsSource = st;
                //MessageBox.Show(r.main.ToString());
            }
            fromauto.PopulateComplete();
        
        }

        private void toauto_Populating(object sender, PopulatingEventArgs e)
        {
            if (checkNetworkConnection())
            {
                WebClient w = new WebClient();

                //this.IsBusy = true;
                w.DownloadStringCompleted += autoto_DownloadStringCompleted;
                w.DownloadStringAsync(new System.Uri("http://api.railwayapi.com/suggest_station/name/" + toauto.Text + "/apikey/thnxp9240/"));


            }


        }

        private void autoto_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
           // throw new NotImplementedException();
            if (e.Error == null)
            {
                spot_your_train_2.Models.Sname sm = null;
                sm = JsonConvert.DeserializeObject<spot_your_train_2.Models.Sname>(e.Result);
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
            if (fromauto.Text == "")
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
                    bool b = checkNetworkConnection();
                    if (!b)
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

        private void brev_Click(object sender, RoutedEventArgs e)
        {
            if (fromauto.Text != "" || toauto.Text != "")
            {
                string temp;
                temp = fromauto.Text;
                fromauto.Text = toauto.Text;
                toauto.Text = temp;

            }

        }
          
    }
      
}