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
    public class USJobGetDataEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public USJobGetDataEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class USJobGetSubPostsEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public USJobGetSubPostsEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class USJobInsertMainPostEvent
    {
        public bool IsSuccess;
        public USJobInsertMainPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class USJobInsertReplyPostEvent
    {
        public bool IsSuccess;
        public USJobInsertReplyPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class USJobGetFiltersEvent
    {

        public ObservableCollection<Dictionary<String, String>> Output;
        public USJobGetFiltersEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }
}
