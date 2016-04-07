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
using NRIUturn.DataModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NRIUturn.Events
{
    public class GetIndianCitiesEvent
    {
        public ObservableCollection<Dictionary<String, String>> IndianCities;
        public GetIndianCitiesEvent(ObservableCollection<Dictionary<String, String>> indianCities)
        {
            IndianCities = indianCities;
        }
    }

    public class GetUSCitiesEvent
    {
        public ObservableCollection<Dictionary<String, String>> USCities;
        public GetUSCitiesEvent(ObservableCollection<Dictionary<String, String>> usCities)
        {
            USCities = usCities;
        }
    }

    public class GetUSStatesEvent
    { 
        public ObservableCollection<Dictionary<String, String>> USStates;
        public GetUSStatesEvent(ObservableCollection<Dictionary<String, String>> usStates)
        {
            USStates = usStates;
        }
    }

    public class GetIndianStatesEvent
    {
        public ObservableCollection<Dictionary<String, String>> IndianStates;
        public GetIndianStatesEvent(ObservableCollection<Dictionary<String, String>> indianStates)
        {
            IndianStates = indianStates;
        }
    }
}
