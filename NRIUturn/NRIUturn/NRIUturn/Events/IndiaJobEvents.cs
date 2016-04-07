﻿using System;
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
    public class IndiaJobGetDataEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public IndiaJobGetDataEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class IndiaJobGetSubPostsEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public IndiaJobGetSubPostsEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class IndiaJobInsertMainPostEvent
    {
        public bool IsSuccess;
        public IndiaJobInsertMainPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class IndiaJobInsertReplyPostEvent
    {
        public bool IsSuccess;
        public IndiaJobInsertReplyPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class IndiaJobGetFiltersEvent
    {

        public ObservableCollection<Dictionary<String, String>> Output;
        public IndiaJobGetFiltersEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }
}
