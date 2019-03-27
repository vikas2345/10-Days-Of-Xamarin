using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamCross10.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Day2_Page : ContentPage
    {
        public Day2_Page()
        {
            InitializeComponent();
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            entTitle.Text = string.Empty;
            edtrExperience.Text = string.Empty;
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