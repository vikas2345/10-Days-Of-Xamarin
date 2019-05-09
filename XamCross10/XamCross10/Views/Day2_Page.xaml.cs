using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Plugin.Geolocator.Abstractions;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamCross10.Common;
using XamCross10.Models;
using static XamCross10.Models.SearchAPI_FourSquareResponseModel;

namespace XamCross10.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Day2_Page : ContentPage
    {
        Experience experience;

        public Day2_Page()
        {
            InitializeComponent();
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            experience = new Experience() {
                Title = entTitle.Text,
                Content = edtrExperience.Text,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                VenueName = selectedVenueName.Text,
                VenueCategory = selectedVenueCategory.Text,
                VenueLat = float.Parse(selectedVenueCoordinates.Text.Split(',')[0]),
                VenueLng = float.Parse(selectedVenueCoordinates.Text.Split(',')[1])

            };

            int insertedItems = 0;
            //add sqlite
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(App.DatabasePath))
            {
                sQLiteConnection.CreateTable<Experience>();
                sQLiteConnection.Insert(experience);
                insertedItems++;
            };

            if(insertedItems > 0)
            {
                //clear the text fields
                entTitle.Text = string.Empty;
                edtrExperience.Text = string.Empty;
                selectedVenueStackLayout.IsVisible = false;
                selectedVenueName.Text = string.Empty;
                selectedVenueCategory.Text = string.Empty;
                selectedVenueCoordinates.Text = string.Empty;

                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Error", "There was an error inserting the Experience, please try again", "Ok");
            }
        }

        private void checkBtnShouldbeEnabled()
        {
            btnSave.IsEnabled = false;

            if (!string.IsNullOrWhiteSpace(entTitle.Text) && !string.IsNullOrWhiteSpace(edtrExperience.Text))
            {
                btnSave.IsEnabled = true;
            }
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            //Navigation.PopAsync();
            Navigation.PopToRootAsync();
        }

        private void EntTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBtnShouldbeEnabled();
        }

        private void EdtrExperience_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBtnShouldbeEnabled();
        }

        private async void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchEntry.Text))
            {
                /*string url = $"<https://api.foursquare.com/v2/venues/search?ll={position.Latitude},{position.Longitude >}
                 * &radius=500&query={searchEntry.Text}&limit=3&client_id={Helpers.Constants.FOURSQR_CLIENT_ID}&
                 * client_secret={Helpers.Constants.FOURSQR_CLIENT_SECRET}&v={DateTime.Now.ToString("yyyyMMdd")}";
            */
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string json = await client.GetStringAsync("http://demo1418609.mockable.io/check");

                        SearchAPI_FourSquareResponseModel results = JsonConvert.DeserializeObject<SearchAPI_FourSquareResponseModel>(json);
                        venueListView.IsVisible = true;
                        venueListView.ItemsSource = results.response.venues;
                    };
                }
                else
                {
                    await DisplayAlert("Alert", "No Internet Connection", "Oke");
                }
            }

            else
            {
                venueListView.IsVisible = false;
            }
        }


        //https://api.foursquare.com/v2/venues/search?ll=12.971599,77.594566&" +
        //    "client_id=BV52JG3OG0HCL1E2GNLU3XM55BASQXNX0CY1NBREG1BTDJ2O&" +
        //    "client_secret=0HOZHFM3FOFGGE2SMKJ0N2LCVWEYK05TUH2IVL5YANXZYDGY&" +
        //    "v=20190326



        string GetParamaters(Position position, DateTime dateTime)
        {
            List<KeyValuePair<string, string>> queryParameter = new List<KeyValuePair<string, string>>();
            queryParameter.Add(new KeyValuePair<string, string>("Latitude", position.Latitude.ToString()));
            queryParameter.Add(new KeyValuePair<string, string>("Longitude", position.Longitude.ToString()));

            return PrepareURL(queryParameter);
        }

        private string PrepareURL(List<KeyValuePair<string, string>> queryParameter)
        {
            string PreparedURL = Constants.FourSquare_APIBaseUrl + Constants.Venues + Constants.Search;
            string LocationText = "?ll";
            foreach (var item in queryParameter)
            {
                switch (item.Key)
                {
                    case "Latitude":
                        PreparedURL = PreparedURL + LocationText + item.Value + "," ;
                        break;
                    case "Longitude":
                        PreparedURL = PreparedURL + item.Value;
                        break;

                    default:
                        break;
                }
            }

            var URL = PreparedURL + Constants.client_id + Constants.FourSquare_clientID+Constants.amPerSand
                + Constants.client_secret + Constants.FourSquare_clientSecret+Constants.amPerSand + DateTime.Now.ToString("yyyyMMdd");

            return URL;
       }

        private void VenueListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(venueListView.SelectedItem != null)
            {
                selectedVenueStackLayout.IsVisible = true;
                searchEntry.Text = string.Empty;
                venueListView.IsVisible = false;

                Venue venue = venueListView.SelectedItem as Venue;
                selectedVenueName.Text = venue.name;
                selectedVenueCategory.Text = venue.categories.FirstOrDefault()?.name;
                selectedVenueCoordinates.Text = $"{ venue.location.lat: 0.0000},{venue.location.lng:0.0000}";
            }
            else
            {
                selectedVenueStackLayout.IsVisible = false;
            }
        }
    }
}