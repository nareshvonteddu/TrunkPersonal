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
using Caliburn.Micro;
using NRIUturn.Events;
using NRIUturn.DataAccess;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using NRIUturn.DataModels;

namespace NRIUturn.ViewModels
{
    public class LoginPageViewModel : Screen, IHandle<InsertUserEvent>, IHandle<LoginUserEvent>, ILoginPageViewModel
    {
        private string _loginUserName = string.Empty;
        public string LoginUserName
        {
            get { return _loginUserName; }
            set { _loginUserName = value; NotifyOfPropertyChange(() => LoginUserName); }
        }

        private string _loginPassword = string.Empty;
        public string LoginPassword
        {
            get { return _loginPassword; }
            set { _loginPassword = value; NotifyOfPropertyChange(() => LoginPassword); }
        }

        private string _loginErrorMessage = string.Empty;
        public string LoginErrorMessage
        {
            get { return _loginErrorMessage; }
            set { _loginErrorMessage = value; NotifyOfPropertyChange(() => LoginErrorMessage); }
        }


        private string _registerErrorMessage = string.Empty;
        public string RegisterErrorMessage
        {
            get { return _registerErrorMessage; }
            set { _registerErrorMessage = value; NotifyOfPropertyChange(() => RegisterErrorMessage); }
        }

        private string _registerUserName = string.Empty;
        public string RegisterUserName
        {
            get { return _registerUserName; }
            set { _registerUserName = value; NotifyOfPropertyChange(() => RegisterUserName); }
        }

        private string _registerPassword = string.Empty;
        public string RegisterPassword
        {
            get { return _registerPassword; }
            set { _registerPassword = value; NotifyOfPropertyChange(() => RegisterPassword); }
        }

        IEventAggregator _eventAgregator;
        public IEventAggregator EventAgregator
        {
            get { return _eventAgregator; }
            set { _eventAgregator = value; }
        }
        
        IDataLayer _dataLayer;
        public IDataLayer DataLayer
        {
            get { return _dataLayer; }
            set { _dataLayer = value; }
        }

        IUser _user;
        //public User User
        //{
        //    get { return _user; }
        //    set { _user = value; NotifyOfPropertyChange(() => User); }
        //}

        public LoginPageViewModel(IDataLayer dataLayer, IEventAggregator eventAgregator, IUser user)
        {
            DisplayName = "Login";
            EventAgregator = eventAgregator;
            EventAgregator.Subscribe(this);
            DataLayer = dataLayer;
            _user = user;
        }

        public void RegisterUser()
        {
            DataLayer.InsertUser(RegisterUserName, RegisterPassword);
        }

        public void Handle(InsertUserEvent message)
        {            
            bool isSuccess = message.IsSuccess;
            ObservableCollection<Dictionary<string, string>> result = message.Output;
            if (isSuccess)
            {
                _user.UserID = Convert.ToInt64(result[0]["UserID"]);
                _user.UserName = RegisterUserName;
                RegisterErrorMessage = string.Empty;
                this.EventAgregator.Publish(new LoginEvent());
                if (this.IsActive)
                {
                    this.TryClose();
                }
            }
            else
            {
                RegisterErrorMessage = "User Name already exists.";
            }
        }

        public void Login()
        {
            DataLayer.LoginUser(LoginUserName, LoginPassword);
        }

        public void Handle(LoginUserEvent message)
        {
            ObservableCollection<Dictionary<string, string>> result = message.Output;
            if (result.Count > 0)
            {
                _user.UserID = Convert.ToInt64(result[0]["ID"]);
                _user.UserName = result[0]["UserName"].ToString();
                LoginErrorMessage = string.Empty;
                this.EventAgregator.Publish(new LoginEvent());
                if (this.IsActive)
                {
                    this.TryClose();
                }
            }
            else
            {
                LoginErrorMessage = "User Name and Password doesn't exist.";
            }
        }

        public void CheckAvailability()
        {
            DataLayer.CheckAvailability(RegisterUserName);
        }
    }

    public interface ILoginPageViewModel
    { }
}
