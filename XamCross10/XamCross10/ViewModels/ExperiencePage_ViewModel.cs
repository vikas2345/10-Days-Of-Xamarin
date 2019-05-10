using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using XamCross10.Models;

namespace XamCross10.ViewModels
{
    public class ExperiencePage_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties

        

        #endregion

    }
}
