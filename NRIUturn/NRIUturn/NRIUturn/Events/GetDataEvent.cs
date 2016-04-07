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
    public class GetDataEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public GetDataEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class GetSubPostsEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public GetSubPostsEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class InsertMainPostEvent
    {
        public bool IsSuccess;
        public InsertMainPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class InsertReplyPostEvent
    {
        public bool IsSuccess;
        public InsertReplyPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
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

    public class CurrencyConverterEvent
    {
        public double DollarValue;
        public CurrencyConverterEvent(double dollarValue)
        {
            DollarValue = dollarValue;
        }
    }

    //public interface IGetDataEvent<GetDataEvent> : IHandle
    //{ 
    //    List<Dictionary<String, String>> GetData();
    //}
}
