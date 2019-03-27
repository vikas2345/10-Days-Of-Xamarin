using System;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamCross10.Models;

namespace XamCross10.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Day2_Page : ContentPage
    {
        Experience experience;
        SQLiteAsyncConnection sQLiteAsyncConnection = new SQLiteAsyncConnection("asdf");

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
                UpdatedAt = DateTime.Now
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

        }

        private void EntTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBtnShouldbeEnabled();
        }

        private void EdtrExperience_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBtnShouldbeEnabled();
        }
    }
}