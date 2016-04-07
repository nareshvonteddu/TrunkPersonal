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
    public class Post : PropertyChangedBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyOfPropertyChange(() => Id); }
        }

        private int _moduleID;
        public int ModuleID
        {
            get { return _moduleID; }
            set { _moduleID = value; NotifyOfPropertyChange(() => ModuleID); }
        }

        private string _postedBy = string.Empty;
        public string PostedBy
        {
            get { return _postedBy; }
            set { _postedBy = value; NotifyOfPropertyChange(() => PostedBy); }
        }

        private string _postDate = string.Empty;
        public string PostDate
        {
            get { return _postDate; }
            set { _postDate = value; NotifyOfPropertyChange(() => PostDate); }
        }

        private string _postString = string.Empty;
        public string PostString
        {
            get { return _postString; }
            set { _postString = value; NotifyOfPropertyChange(() => PostString); }
        }

        private string _postSubject = string.Empty;
        public string PostSubject
        {
            get { return _postSubject; }
            set { _postSubject = value; NotifyOfPropertyChange(() => PostSubject); }
        }

        private ObservableCollection<Post> _children = new ObservableCollection<Post>();
        public ObservableCollection<Post> Children
        {
            get { return _children; }
            set { _children = value; NotifyOfPropertyChange(() => Children); }
        }

        private string filterID;
        public string FilterID
        {
            get { return filterID; }
            set { filterID = value; NotifyOfPropertyChange(() => FilterID); }
        }

        private int cityID;
        public int CityID
        {
            get { return cityID; }
            set { cityID = value; NotifyOfPropertyChange(() => CityID); }
        }

        private int stateID;
        public int StateID
        {
            get { return stateID; }
            set { stateID = value; NotifyOfPropertyChange(() => StateID); }
        }
    }
}
