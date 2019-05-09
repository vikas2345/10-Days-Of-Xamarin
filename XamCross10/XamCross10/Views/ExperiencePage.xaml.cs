
/* READ ME
 * Class -
 * 
 * ------------Methods-------
 * ToolbarItem_NewClicked
    Geolocator_PositionChanged
    GetLocationPermission
    GetLocation
    ReadExperience
 * 
 * sadfas*/





using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamCross10.Models;
using XamCross10.ViewModels;

namespace XamCross10.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExperiencePage : ContentPage
    {
        Position position;
        IGeolocator geolocator = CrossGeolocator.Current;
        ExperiencePage_ViewModel viewModel;

        public ExperiencePage()
        {
            InitializeComponent();
            BindingContext = viewModel;

        }

        private void ToolbarItem_NewClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Day2_Page());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            geolocator.PositionChanged += Geolocator_PositionChanged;
            GetLocationPermission();
            ReadExperience();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            geolocator.StopListeningAsync();
        }

        private void Geolocator_PositionChanged(object sender, PositionEventArgs e)
        {
            position = e.Position;
        }

        private async void GetLocationPermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            Console.WriteLine(status);
            if (!status.Equals(PermissionStatus.Granted.ToString()))
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                {
                    await DisplayAlert("Need your permission", "We need to access your location", "Ok");
                }

                var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                if (result.ContainsKey(Permission.LocationWhenInUse))
                    status = result[Permission.LocationWhenInUse];
            }
            if (status == PermissionStatus.Granted)
            {
                GetLocation();
            }
            else
            {
                await DisplayAlert("Access to location denied", "We don't have access to your location", "Ok");
            }
        }

        private async void GetLocation()
        {
            //var locator = CrossGeolocator.Current;
            position = await geolocator.GetPositionAsync();

            await geolocator.StartListeningAsync(TimeSpan.FromMinutes(30), 500);
        }

        private void ReadExperience()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Experience>();

                List<Experience> experiences = conn.Table<Experience>().ToList();
                experiencesListView.ItemsSource = experiences;
            };
        }
    }
}