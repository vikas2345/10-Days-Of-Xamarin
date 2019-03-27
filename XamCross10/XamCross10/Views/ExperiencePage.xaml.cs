using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamCross10.Models;

namespace XamCross10.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExperiencePage : ContentPage
    {
        Position position;
        IGeolocator geolocator = CrossGeolocator.Current;

        public ExperiencePage()
        {
            InitializeComponent();
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
            if (!status.Equals(PermissionStatus.Granted))
            {

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
            
            using(SQLiteConnection conn = new SQLiteConnection(App.DatabasePath)) {
                conn.CreateTable<Experience>();

                List<Experience> experiences = conn.Table<Experience>().ToList();
                experiencesListView.ItemsSource = experiences;
            };
        }
    }
}