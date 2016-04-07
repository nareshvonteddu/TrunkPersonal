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
    public class IndiaHouseGetDataEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public IndiaHouseGetDataEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class IndiaHouseGetSubPostsEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public IndiaHouseGetSubPostsEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class IndiaHouseInsertMainPostEvent
    {
        public bool IsSuccess;
        public IndiaHouseInsertMainPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class IndiaHouseInsertReplyPostEvent
    {
        public bool IsSuccess;
        public IndiaHouseInsertReplyPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class IndiaHouseGetFiltersEvent
    {

        public ObservableCollection<Dictionary<String, String>> Output;
        public IndiaHouseGetFiltersEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }
}
