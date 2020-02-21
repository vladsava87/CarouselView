using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Internals;

namespace WizzardControl
{
    [Preserve (AllMembers = true)]
    public class TaxWizzardPage : INotifyPropertyChanged //BaseModel
    {
        public int Order { get; set; }
        public string Title { get; set; }

        private string _info;
        public string Info
        {
            get => _info;
            set
            {
                _info = value;
                OnPropertyChanged();
            }
        }

        private string _info2;
        public string Info2
        {
            get => _info2;
            set
            {
                _info2 = value;
                OnPropertyChanged();
            }
        }

        public string Icon { get; set; }

        public event EventHandler<object> ResultSelected;

        public virtual void OnResultSelected(object e)
        {
            EventHandler<object> handler = ResultSelected;
            handler?.Invoke(this, e);
        }

        public object Result { get; set; }

        //public WizzardPageTypes PageType { get; set; }

        public object Items  { get; set; }

        //for checkbox, select, radio
        public Dictionary<int, string> ListValues { get; set; }

        public bool ShowTwoColumns { get; set; }

        //for Slider
        public double Min { get; set; }
        public double Max { get; set; }
        public double Step { get; set; }

        //for passing values form page to page
        public object Parameters { get; set; }

        public void SetResultSelectedEventHandler(EventHandler<object> resultSelectedEvent)
        {
            ResultSelected = resultSelectedEvent;
        }

        public TaxWizzardPage( EventHandler<object> resultSelectedEvent = null)
        {
            //PageType = pageType;
            ResultSelected = resultSelectedEvent;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
