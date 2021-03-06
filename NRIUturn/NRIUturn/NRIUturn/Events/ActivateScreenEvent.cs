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
using Caliburn.Micro;

namespace NRIUturn.Events
{
    public class ActivateScreenEvent
    {
        public ActivateScreenEvent(string displayName)
        {
            DisplayName = displayName;
        }
        public string DisplayName{ get; private set; }
    }
}
