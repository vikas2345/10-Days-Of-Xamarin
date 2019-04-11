using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Plugin.BLE.Abstractions.Contracts;

namespace XamCross10.ViewModels
{
    public class DeviceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IDevice _nativeDevice;

        public IDevice NativeDevice
        {
            get { return _nativeDevice; }
            set {
                _nativeDevice = value;
                RaisePropertyChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string caller ="")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
            
        }
    }
}
