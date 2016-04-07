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
    public class USHouseGetDataEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public USHouseGetDataEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class USHouseGetSubPostsEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public USHouseGetSubPostsEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class USHouseInsertMainPostEvent
    {
        public bool IsSuccess;
        public USHouseInsertMainPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class USHouseInsertReplyPostEvent
    {
        public bool IsSuccess;
        public USHouseInsertReplyPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class USHouseGetFiltersEvent
    {

        public ObservableCollection<Dictionary<String, String>> Output;
        public USHouseGetFiltersEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }
}
