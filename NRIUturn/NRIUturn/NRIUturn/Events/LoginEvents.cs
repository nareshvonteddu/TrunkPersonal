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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Caliburn.Micro;


namespace NRIUturn.Events
{
    public class LoginEvent
    {

    }

    public class LoginUserEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public LoginUserEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class InsertUserEvent
    {
        public bool IsSuccess;
        public ObservableCollection<Dictionary<String, String>> Output;
        public InsertUserEvent(bool isSuccess, ObservableCollection<Dictionary<String, String>> output)
        {
            IsSuccess = isSuccess;
            Output = output;
        }

    }

    public class CheckAvailableEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public CheckAvailableEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }
}
