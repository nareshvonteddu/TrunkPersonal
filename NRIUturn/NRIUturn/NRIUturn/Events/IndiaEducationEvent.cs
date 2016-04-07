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
    public class IndiaEducationGetDataEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public IndiaEducationGetDataEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class IndiaEducationGetSubPostsEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public IndiaEducationGetSubPostsEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class IndiaEducationInsertMainPostEvent
    {
        public bool IsSuccess;
        public IndiaEducationInsertMainPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class IndiaEducationInsertReplyPostEvent
    {
        public bool IsSuccess;
        public IndiaEducationInsertReplyPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class IndiaEducationGetFiltersEvent
    {

        public ObservableCollection<Dictionary<String, String>> Output;
        public IndiaEducationGetFiltersEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }
}
