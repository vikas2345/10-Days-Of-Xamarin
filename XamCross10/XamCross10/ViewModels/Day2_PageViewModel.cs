using System;
using System.ComponentModel;
using System.Windows.Input;
using XamCross10.Models;

namespace XamCross10.ViewModels
{
    public class Day2_PageViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
                OnPropertyChanged("CanSave");
            }
        }

        private Venue selectedVenue;
        public Venue SelectedVenue
        {
            get { return selectedVenue; }
            set
            {
                selectedVenue = value;
                if (selectedVenue != null)
                {
                    ShowVenues = false;
                    Query = string.Empty;
                }

                OnPropertyChanged("SelectedVenue");
                OnPropertyChanged("ShowSelectedVenue");
            }
        }

        private string query;
        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged("Content");
                OnPropertyChanged("CanSave");
            }
        }

        public bool CanSave
        {
            get { return !string.IsNullOrWhiteSpace("Title") && !string.IsNullOrWhiteSpace("Content"); }
        }

        public bool ShowSelectedVenue
        {
            get { return SelectedVenue != null; }
        }

        private bool showVenues;
        public bool ShowVenues
        {
            get { return showVenues; }
            set
            {
                showVenues = value;
                OnPropertyChanged("ShowVenues");
            }
        }

    }
}
