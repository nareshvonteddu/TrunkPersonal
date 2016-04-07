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
    public class State
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class IndianStates : ObservableCollection<State>
    { 
    
    }

    public class USStates : ObservableCollection<State>
    {

    }
}
