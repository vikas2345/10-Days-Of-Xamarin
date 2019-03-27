using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamCross10.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Day1_Page : ContentPage
	{
		public Day1_Page ()
		{
			InitializeComponent ();
		}

        private void BtnClick_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entName.Text))
            {
                DisplayAlert("Error", "Please enter UserName ", "Oke");
                lblGreeting.Text = null;
            }
            else
                lblGreeting.Text = string.Format("Hello \"{0}\", Welcome to 10 Days of Xamarin", entName.Text);
            }
    }
}