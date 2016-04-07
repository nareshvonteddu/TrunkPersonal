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
    public class ImigrationGetDataEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public ImigrationGetDataEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class ImigrationGetSubPostsEvent
    {
        public ObservableCollection<Dictionary<String, String>> Output;
        public ImigrationGetSubPostsEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }

    public class ImigrationInsertMainPostEvent
    {
        public bool IsSuccess;
        public ImigrationInsertMainPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class ImigrationInsertReplyPostEvent
    {
        public bool IsSuccess;
        public ImigrationInsertReplyPostEvent(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class ImigrationGetFiltersEvent
    {
        
        public ObservableCollection<Dictionary<String, String>> Output;
        public ImigrationGetFiltersEvent(ObservableCollection<Dictionary<String, String>> output)
        {
            Output = output;
        }
    }
    //public interface IGetDataEvent<GetDataEvent> : IHandle
    //{ 
    //    List<Dictionary<String, String>> GetData();
    //}
}
