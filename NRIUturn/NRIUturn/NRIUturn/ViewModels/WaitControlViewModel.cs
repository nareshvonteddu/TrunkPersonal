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

namespace NRIUturn.ViewModels
{
    public class WaitControlViewModel : Screen,  IWaitControlViewModel
    {
        public WaitControlViewModel()
        {
            DisplayName = "WaitControlViewModel";
        }
    }

    public interface IWaitControlViewModel: IScreen
    { 
    
    }
}
