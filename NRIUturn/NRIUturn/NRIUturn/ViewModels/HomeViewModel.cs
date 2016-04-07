using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Caliburn.Micro;
using NRIUturn.Events;
using NRIUturn.DataAccess;
using NRIUturn.DataModels;

namespace NRIUturn.ViewModels
{
    public class HomeViewModel : Conductor<IScreen>.Collection.OneActive, IHomeViewModel, IHandle<CurrencyConverterEvent>
    {
        IEventAggregator events;
        IDataLayer _dataLayer;
        IUser _user;
        private double _dollarRate;
        USCities USCities;
        IndianCities IndianCities;

        public double DollarRate
        {
            get { return _dollarRate; }
            set { _dollarRate = value; NotifyOfPropertyChange(() => DollarRate); }
        }

        public HomeViewModel(IEventAggregator Events, IDataLayer dataLayer, IUser user, USCities usCities, IndianCities indianCities)
        {
            DisplayName = "Home";           
            this.events = Events;
            events.Subscribe(this);
            _dataLayer = dataLayer;
            _user = user;
            IndianCities = indianCities;
            USCities = usCities;
            //DollarRate = dollarRate;
        }

        private void ActivateTab(string displayName)
        {
            events.Publish(new ActivateScreenEvent(displayName));
        }

        public void IndiaEducation()
        {
            ActivateTab("IndiaEducation");
        }

        public void USEducation()
        {
            ActivateTab("USEducation");
        }

        public void IndiaHouse()
        {
            ActivateTab("IndiaHouse");
        }

        public void USHouse()
        {
            ActivateTab("USHouse");
        }

        public void IndiaJob()
        {
            ActivateTab("IndiaJob");
        }

        public void USJob()
        {
            ActivateTab("USJob");
        }

        public void Immigration()
        {
            ActivateTab("Imigration");
        }

        public void Handle(CurrencyConverterEvent message)
        {
            DollarRate = message.DollarValue;
        }
    }

    public interface IHomeViewModel : IScreen
    {
    }
}
