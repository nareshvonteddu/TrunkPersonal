﻿#pragma checksum "C:\NRIUturn\NRIUturn\NRIUturn\Views\IndiaJobView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "347939A47A62122D76C02A4D59B9F81D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace NRIUturn.Views {
    
    
    public partial class IndiaJobView : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button NewPostClick;
        
        internal System.Windows.Controls.Button ReplyPost;
        
        internal System.Windows.Controls.Button SaveNewPost;
        
        internal System.Windows.Controls.Button CancelNewPost;
        
        internal System.Windows.Controls.ComboBox Filters;
        
        internal System.Windows.Controls.ComboBox States;
        
        internal System.Windows.Controls.ComboBox Cities;
        
        internal System.Windows.Controls.DataGrid ContainerListBox;
        
        internal System.Windows.Controls.TextBox BodyTextBox;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/NRIUturn;component/Views/IndiaJobView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.NewPostClick = ((System.Windows.Controls.Button)(this.FindName("NewPostClick")));
            this.ReplyPost = ((System.Windows.Controls.Button)(this.FindName("ReplyPost")));
            this.SaveNewPost = ((System.Windows.Controls.Button)(this.FindName("SaveNewPost")));
            this.CancelNewPost = ((System.Windows.Controls.Button)(this.FindName("CancelNewPost")));
            this.Filters = ((System.Windows.Controls.ComboBox)(this.FindName("Filters")));
            this.States = ((System.Windows.Controls.ComboBox)(this.FindName("States")));
            this.Cities = ((System.Windows.Controls.ComboBox)(this.FindName("Cities")));
            this.ContainerListBox = ((System.Windows.Controls.DataGrid)(this.FindName("ContainerListBox")));
            this.BodyTextBox = ((System.Windows.Controls.TextBox)(this.FindName("BodyTextBox")));
        }
    }
}

