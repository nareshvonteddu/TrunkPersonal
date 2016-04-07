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
using System.Collections.ObjectModel;

namespace NRIUturn.DataModels
{
    public class City:ICity
    {   
       public int ID { get; set; }
       public string CityName { get; set; }
       public string State { get; set; }
       public int StateID { get; set; }
    }

    interface ICity
    {
        int ID { get; set; }
        string CityName { get; set; }
        string State { get; set; }
        int StateID { get; set; }
    }

    public class IndianCities : ObservableCollection<City>
    { 
    
    }

    public class USCities : ObservableCollection<City>
    {

    }
}
