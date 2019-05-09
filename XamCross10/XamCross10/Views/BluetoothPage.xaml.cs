using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamCross10.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BluetoothPage : ContentPage
	{
        IBluetoothLE ble;
        IAdapter adapter;
        ObservableCollection<IDevice> deviceList;

		public BluetoothPage ()
		{
			InitializeComponent ();

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            deviceList = new ObservableCollection<IDevice>();
            lv.ItemsSource = deviceList;
		}

        private void btnStatus_Clicked(object sender, EventArgs e)
        {
            var state = ble.State;
            this.DisplayAlert("Notice", state.ToString(), "Oke");

            if(state == BluetoothState.Off)
            {
                txtErrorBle.BackgroundColor = Color.Red;
                txtErrorBle.TextColor = Color.White;
                txtErrorBle.Text = "Bluetooth is turned off! Turn it On";
            }
        }

        void btnStatuc_Clicked()
        {

        }

        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            try
            {
                deviceList.Clear();
                adapter.DeviceDiscovered += (s, a) =>
                {
                    deviceList.Add(a.Device);
                };

                //We have to test if the device is scanning 
                if (!ble.Adapter.IsScanning)
                {
                    await adapter.StartScanningForDevicesAsync();

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Notice", ex.Message.ToString(), "Error !");
            }

        }

        private async void btnConnect_Clicked(object sender, EventArgs e)
        {
            try
            {
                deviceList.Clear();

                adapter.DeviceDiscovered += (s, a) =>
                {
                    deviceList.Add(a.Device);
                };

                await adapter.StartScanningForDevicesAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }

        //private void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
        //{
        //    deviceList.Add(e.Device);
        //}

        private async void btnKnowConnect_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnGetServices_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnGetcharacters_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnGetRW_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnDescriptors_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnDescRW_Clicked(object sender, EventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}