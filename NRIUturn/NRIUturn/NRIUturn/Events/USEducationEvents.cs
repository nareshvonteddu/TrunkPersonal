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
    public class USEducationGetDataEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public USEducationGetDataEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class USEducationGetSubPostsEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public USEducationGetSubPostsEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class USEducationInsertMainPostEvent
    {
        public bool IsSuccess;
        public USEducationInsertMainPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class USEducationInsertReplyPostEvent
    {
        public bool IsSuccess;
        public USEducationInsertReplyPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class USEducationGetFiltersEvent
    {

        public ObservableCollection<Dictionary<String, String>> Output;
        public USEducationGetFiltersEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }
}
