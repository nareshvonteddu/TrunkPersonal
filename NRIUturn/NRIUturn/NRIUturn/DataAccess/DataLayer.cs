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
using System.Collections;
using NRIUturn.DataLayerService;
using Caliburn.Micro;
using NRIUturn.Events;
using System.Collections.Generic;
using NRIUturn.CurrencyService;
using NRIUturn.ViewModels;

namespace NRIUturn.DataAccess
{
    public class DataLayer : IDataLayer
    {
        private ServiceClient service;
        IEventAggregator EventAgregator;
        CurrencyConvertorSoapClient currencyConverterClient;
        WindowManager windowManager = new WindowManager();
        //IWaitControlViewModel sc = IoC.Get<IWaitControlViewModel>();
        IScreen sc;
        public DataLayer(IEventAggregator eventAgregator)
        {
            this.EventAgregator = eventAgregator;
            EventAgregator.Subscribe(this);
            service = new ServiceClient();
            currencyConverterClient = new CurrencyConvertorSoapClient();
            currencyConverterClient.OpenAsync();
            currencyConverterClient.OpenCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(currencyConverterClient_OpenCompleted);
            currencyConverterClient.ConversionRateCompleted += new EventHandler<ConversionRateCompletedEventArgs>(currencyConverterClient_ConversionRateCompleted);
            service.CallProcedureCompleted += new EventHandler<CallProcedureCompletedEventArgs>(service_CallProcedureCompleted);
        }

        void currencyConverterClient_OpenCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            currencyConverterClient.ConversionRateAsync(Currency.USD, Currency.INR);
        }

        void currencyConverterClient_ConversionRateCompleted(object sender, ConversionRateCompletedEventArgs e)
        {
            EventAgregator.Publish(new CurrencyConverterEvent(e.Result));
            currencyConverterClient.CloseAsync();
        }

        public void GetDollarRate()
        {
            currencyConverterClient.OpenAsync();
        }

        public void GetPostsForModule(string moduleID)
        {
           ShowWaitControl();
           Dictionary<string,string> parameters = new Dictionary<string,string>();
           parameters.Add("@moduleID", moduleID);
            string procName = "GetPostsFor";
           service.CallProcedureAsync(procName,moduleID, parameters);
        }

        private void ShowWaitControl()
        {
            if (sc == null)
            {
                sc = IoC.Get<IScreen>("WaitControlViewModel");
            }
            if (!sc.IsActive)
            {
                windowManager.ShowPopup(sc);
            }
        }

        public void GetSubPostsForModule(string moduleID, string postID)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@moduleID", moduleID);
            parameters.Add("@parentID", postID);
            string procName = "GetSubPostsFor";
            service.CallProcedureAsync(procName,moduleID, parameters);
        }

        public void InsertMainPost(string moduleID, string userID, string postString, string postSubject, string filterID, string cityID = "0", string stateID = "0")
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@moduleID", moduleID);
            parameters.Add("@userID", userID);
            parameters.Add("@postString", postString);
            parameters.Add("@postSubject", postSubject);
            parameters.Add("@filterID", filterID);
            parameters.Add("@cityID", cityID);
            parameters.Add("@stateID", stateID);
            string procName = "InsertMainPost";
            service.CallProcedureAsync(procName,moduleID, parameters);
        }

        public void InsertReplyPost(string moduleID,string parentID, string userID, string postString)
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@moduleID", moduleID);
            parameters.Add("@parentID", parentID);
            parameters.Add("@userID", userID);
            parameters.Add("@postString", postString);
            string procName = "InsertReplyPost";
            service.CallProcedureAsync(procName,moduleID, parameters);
        }

        public void InsertUser(string userName, string password)
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@userName", userName);
            parameters.Add("@password", password);
            string procName = "InsertUser";
            service.CallProcedureAsync(procName,string.Empty, parameters);
        }

        public void LoginUser(string userName, string password)
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@userName", userName);
            parameters.Add("@password", password);
            string procName = "LoginUser";
            service.CallProcedureAsync(procName, string.Empty, parameters);
        }

        public void CheckAvailability(string userName)
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@userName", userName);
            string procName = "CheckAvailabiity";
            service.CallProcedureAsync(procName, string.Empty, parameters);
        }

        public void GetFiltersByModule(string moduleID)
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@moduleID", moduleID);
            string procName = "GetFiltersByModule";
            service.CallProcedureAsync(procName, moduleID, parameters);
        }

        public void GetUsCities()
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string procName = "GetUSCities";
            service.CallProcedureAsync(procName, string.Empty, parameters);
        }

        public void GetIndianCities()
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string procName = "GetIndianCities";
            service.CallProcedureAsync(procName, string.Empty, parameters);
        }

        public void GetUSStates()
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string procName = "GetUSStates";
            service.CallProcedureAsync(procName, string.Empty, parameters);
        }

        public void GetIndianStates()
        {
            ShowWaitControl();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string procName = "GetIndianStates";
            service.CallProcedureAsync(procName, string.Empty, parameters);
        }

        void service_CallProcedureCompleted(object sender, CallProcedureCompletedEventArgs e)
        {
            if (sc != null && sc.IsActive)
            {
                sc.TryClose();
            }
            if (e.sql == "GetPostsFor")
            {
                if (e.isSuccess)
                {
                    switch (e.module)
                    {
                        case "1":
                            EventAgregator.Publish(new USEducationGetDataEvent(e.Result));
                            break;
                        case "2":
                            EventAgregator.Publish(new IndiaEducationGetDataEvent(e.Result));
                            break;
                        case "3":
                            EventAgregator.Publish(new USJobGetDataEvent(e.Result));
                            break;
                        case "4":
                            EventAgregator.Publish(new IndiaJobGetDataEvent(e.Result));
                            break;
                        case "5":
                            EventAgregator.Publish(new USHouseGetDataEvent(e.Result));
                            break;
                        case "6":
                            EventAgregator.Publish(new IndiaHouseGetDataEvent(e.Result));
                            break;
                        case "7":
                            EventAgregator.Publish(new ImigrationGetDataEvent(e.Result));
                            break;
                        default:
                            break;
                    }
                    
                }
            }
            else if (e.sql == "GetSubPostsFor")
            {
                if (e.isSuccess)
                {
                    switch (e.module)
                    {
                        case "1":
                            EventAgregator.Publish(new USEducationGetSubPostsEvent(e.Result));
                            break;
                        case "2":
                            EventAgregator.Publish(new IndiaEducationGetSubPostsEvent(e.Result));
                            break;
                        case "3":
                            EventAgregator.Publish(new USJobGetSubPostsEvent(e.Result));
                            break;
                        case "4":
                            EventAgregator.Publish(new IndiaJobGetSubPostsEvent(e.Result));
                            break;
                        case "5":
                            EventAgregator.Publish(new USHouseGetSubPostsEvent(e.Result));
                            break;
                        case "6":
                            EventAgregator.Publish(new IndiaHouseGetSubPostsEvent(e.Result));
                            break;
                        case "7":
                            EventAgregator.Publish(new ImigrationGetSubPostsEvent(e.Result));
                            break;
                        default:
                            break;
                    }                    
                }
            }
            else if (e.sql == "InsertMainPost")
            {
                switch (e.module)
                {
                    case "1":
                        EventAgregator.Publish(new USEducationInsertMainPostEvent(e.isSuccess));
                        break;
                    case "2":
                        EventAgregator.Publish(new IndiaEducationInsertMainPostEvent(e.isSuccess));
                        break;
                    case "3":
                        EventAgregator.Publish(new USJobInsertMainPostEvent(e.isSuccess));
                        break;
                    case "4":
                        EventAgregator.Publish(new IndiaJobInsertMainPostEvent(e.isSuccess));
                        break;
                    case "5":
                        EventAgregator.Publish(new USHouseInsertMainPostEvent(e.isSuccess));
                        break;
                    case "6":
                        EventAgregator.Publish(new IndiaHouseInsertMainPostEvent(e.isSuccess));
                        break;
                    case "7":
                        EventAgregator.Publish(new ImigrationInsertMainPostEvent(e.isSuccess));
                        break;
                    default:
                        break;
                } 
            }
            else if (e.sql == "InsertReplyPost")
            {
                switch (e.module)
                {
                    case "1":                        
                        EventAgregator.Publish(new USEducationInsertReplyPostEvent(e.isSuccess));
                        break;
                    case "2":
                        EventAgregator.Publish(new IndiaEducationInsertReplyPostEvent(e.isSuccess));
                        break;
                    case "3":
                        EventAgregator.Publish(new USJobInsertReplyPostEvent(e.isSuccess));
                        break;
                    case "4":
                        EventAgregator.Publish(new IndiaJobInsertReplyPostEvent(e.isSuccess));
                        break;
                    case "5":
                        EventAgregator.Publish(new USHouseInsertReplyPostEvent(e.isSuccess));
                        break;
                    case "6":
                        EventAgregator.Publish(new IndiaHouseInsertReplyPostEvent(e.isSuccess));
                        break;
                    case "7":
                        EventAgregator.Publish(new ImigrationInsertReplyPostEvent(e.isSuccess));
                        break;
                    default:
                        break;
                }                 
            }
            else if (e.sql == "GetFiltersByModule")
            {
                switch (e.module)
                {
                    case "1":
                        EventAgregator.Publish(new USEducationGetFiltersEvent(e.Result));
                        break;
                    case "2":
                        EventAgregator.Publish(new IndiaEducationGetFiltersEvent(e.Result));
                        break;
                    case "3":
                        EventAgregator.Publish(new USJobGetFiltersEvent(e.Result));
                        break;
                    case "4":
                        EventAgregator.Publish(new IndiaJobGetFiltersEvent(e.Result));
                        break;
                    case "5":
                        EventAgregator.Publish(new USHouseGetFiltersEvent(e.Result));
                        break;
                    case "6":
                        EventAgregator.Publish(new IndiaHouseGetFiltersEvent(e.Result));
                        break;
                    case "7":
                        EventAgregator.Publish(new ImigrationGetFiltersEvent(e.Result));
                        break;
                    default:
                        break;
                }
            }
            else if (e.sql == "InsertUser")
            {
                EventAgregator.Publish(new InsertUserEvent(e.isSuccess, e.Result));
            }
            else if (e.sql == "LoginUser" && e.isSuccess)
            {  
                EventAgregator.Publish(new LoginUserEvent(e.Result));
            }
            else if (e.sql == "CheckAvailabiity" && e.isSuccess)
            {
                EventAgregator.Publish(new CheckAvailableEvent(e.Result));
            }
            else if (e.sql == "GetUSCities" && e.isSuccess)
            {
                EventAgregator.Publish(new GetUSCitiesEvent(e.Result));
            }
            else if (e.sql == "GetIndianCities" && e.isSuccess)
            {
                EventAgregator.Publish(new GetIndianCitiesEvent(e.Result));
            }
            else if (e.sql == "GetUSStates" && e.isSuccess)
            {
                EventAgregator.Publish(new GetUSStatesEvent(e.Result));
            }
            else if (e.sql == "GetIndianStates" && e.isSuccess)
            {
                EventAgregator.Publish(new GetIndianStatesEvent(e.Result));
            }
        }

        void service_GetDataCompleted(object sender, GetDataCompletedEventArgs e)
        {
           
        }
    }

    public interface IDataLayer
    {
        void GetDollarRate();
        void GetPostsForModule(string moduleID);
        void GetSubPostsForModule(string moduleID, string postID);
        void InsertMainPost(string moduleID, string userID, string postString, string postSubject, string filterID, string cityID = "0", string stateID = "0");
        void InsertReplyPost(string moduleID, string parentID, string userID, string postString);
        void InsertUser(string userName, string password);
        void LoginUser(string userName, string password);
        void CheckAvailability(string userName);
        void GetFiltersByModule(string moduleID);
        void GetUsCities();
        void GetIndianCities();
        void GetUSStates();
        void GetIndianStates();
    }
}
