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
using NRIUturn.ViewModels;
using NRIUturn.Events;
using NRIUturn.DataAccess;
using NRIUturn.DataModels;

namespace NRIUturn
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<ActivateScreenEvent>, IShell, IHandle<GetUSCitiesEvent>, IHandle<GetIndianCitiesEvent>, IHandle<GetUSStatesEvent>, IHandle<GetIndianStatesEvent>
    {
        IEventAggregator events;
        WindowManager windowManager = new WindowManager();
        IDataLayer dataLayer;
        IUser _user;
        USCities usCities;
        IndianCities indianCities;
        USStates usStates;
        IndianStates indianStates;
        //private double _dollarRate;

        //public double DollarRate
        //{
        //    get { return _dollarRate; }
        //    set { _dollarRate = value; NotifyOfPropertyChange(() => DollarRate); }
        //}

        public IUser User
        {
            get { return _user; }
            set { _user = value; NotifyOfPropertyChange(() => User); }
        }

        public ShellViewModel(IEventAggregator _events, IUser user, IDataLayer _dataLayer, 
            USCities usCities, IndianCities indianCities, USStates usStates, IndianStates indianStates)
        {
            events = _events;
            dataLayer = _dataLayer;
            events.Subscribe(this);
            User = user;
            ActivateTab("Home");
            dataLayer.GetUsCities();
            dataLayer.GetIndianCities();
            dataLayer.GetUSStates();
            dataLayer.GetIndianStates();
            this.usCities = usCities;
            this.indianCities = indianCities;
            this.usStates = usStates;
            this.indianStates = indianStates;
        }

        private void ActivateTab(string displayName)
        {
            bool reusedExistingView = false;
            foreach (IScreen screen in Items)
            {
                if(screen.DisplayName == displayName)
                {
                    ActivateItem(screen);
                    reusedExistingView = true;
                    return;
                }                 
            }
            if (!reusedExistingView)
            {
                IScreen sc = IoC.Get<IScreen>(displayName);
                Items.Add(sc);
                ActivateItem(sc);
                return;
            }
        }

        public void HomeMenu()
        {
            ActivateTab("Home");
        }

        public void IndiaEducation()
        {
            ActivateTab("IndiaEducation");
        }

        public void USEducation()
        {
            ActivateTab("USEducation");
        }

        public void IndiaJob()
        {
            ActivateTab("IndiaJob");
        }

        public void USJob()
        {
            ActivateTab("USJob");
        }

        public void IndiaHouse()
        {
            ActivateTab("IndiaHouse");
        }

        public void USHouse()
        {
            ActivateTab("USHouse");
        }

        public void Immigration()
        {
            ActivateTab("Imigration");
        }

        public void Handle(ActivateScreenEvent message)
        {
            ActivateTab(message.DisplayName);
        }

        public void Login()
        {
            //windowManager.ShowDialog(new LoginPageViewModel((DataLayer)dataLayer, events, User));
            windowManager.ShowDialog(IoC.Get<ILoginPageViewModel>());            
        }

        public void Logout()
        {
            User.UserID = 0;
            User.UserName = string.Empty;
            events.Publish(new LoginEvent());
        }

        void IHandle<GetUSCitiesEvent>.Handle(GetUSCitiesEvent message)
        {
            foreach (var Item in message.USCities)
            {
                usCities.Add(new City() { ID = Convert.ToInt32(Item["ID"]), CityName = Item["City"].ToString(), State = Item["State"], StateID = Convert.ToInt32(Item["StateID"]) });
            }
        }

        void IHandle<GetIndianCitiesEvent>.Handle(GetIndianCitiesEvent message)
        {
            foreach (var Item in message.IndianCities)
            {
               indianCities.Add(new City() { ID = Convert.ToInt32(Item["ID"]), CityName = Item["City"].ToString(), State = Item["State"], StateID = Convert.ToInt32(Item["StateID"]) });
            }
        }

        public void Handle(GetUSStatesEvent message)
        {
            foreach (var Item in message.USStates)
            {
                usStates.Add(new State() { ID = Convert.ToInt32(Item["ID"]), Name = Item["Name"].ToString() });
            }
        }

        public void Handle(GetIndianStatesEvent message)
        {
            foreach (var Item in message.IndianStates)
            {
                indianStates.Add(new State() { ID = Convert.ToInt32(Item["ID"]), Name = Item["Name"].ToString() });
            }
        }
    }

    public interface IShell
    {
    }
}
