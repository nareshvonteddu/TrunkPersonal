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

namespace NRIUturn.DataModels
{
    public class User : PropertyChangedBase, IUser
    {
        private long _userID;
        public long UserID
        {
            get { return _userID; }
            set 
            {
                _userID = value;
                NotifyOfPropertyChange(() => UserID);
                NotifyOfPropertyChange(() => LoginVisible);
                NotifyOfPropertyChange(() => LoggedUserVisible);
            }
        }

        private string _userName = string.Empty;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; NotifyOfPropertyChange(() => UserName); }
        }



        //private Visibility _loginVisible = Visibility.Visible;
        public Visibility LoginVisible
        {
            get
            {
                if (UserID != 0)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {
                
                NotifyOfPropertyChange(() => LoginVisible);
                NotifyOfPropertyChange(() => LoggedUserVisible);
            }
        }

        //private Visibility _loggedUserVisible = Visibility.Collapsed;
        public Visibility LoggedUserVisible
        {
            get
            {
                if (LoginVisible == Visibility.Visible)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {                
                NotifyOfPropertyChange(() => LoggedUserVisible);
            }
        }

    }

    public interface IUser
    {
        long UserID { get; set; }
        string UserName { get; set; }
        Visibility LoginVisible { get; set; }
        Visibility LoggedUserVisible { get; set; }
    }
}
