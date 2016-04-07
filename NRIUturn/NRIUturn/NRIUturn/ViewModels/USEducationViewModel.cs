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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using NRIUturn.DataAccess;
using NRIUturn.DataModels;
using NRIUturn.Helpers;
using System.Linq;

namespace NRIUturn.ViewModels
{
    public class USEducationViewModel : Screen, IUSEducationViewModel, IHandle<USEducationGetDataEvent>, IHandle<USEducationGetSubPostsEvent>, IHandle<USEducationInsertMainPostEvent>, IHandle<USEducationInsertReplyPostEvent>, IHandle<LoginEvent>, IHandle<USEducationGetFiltersEvent>
    {
        IDataLayer _dataLayer;
        ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        Post _selectedPost;
        Post _newPost;
        Visibility _newPostVisible = Visibility.Collapsed;
        Visibility _oldPostsVisible = Visibility.Visible;
        IEventAggregator _eventAgregator;
        string _moduleId;
        Post _replyPost;
        Visibility _replyPostVisible = Visibility.Collapsed;
        Visibility _replyButtonVisible = Visibility.Collapsed;
        Visibility _postButtonVisible = Visibility.Collapsed;
        Visibility _newPostButtonVisible = Visibility.Visible;
        IUser _user;
        ObservableCollection<Post> filteredPosts = new ObservableCollection<Post>();
        BindableCollection<KeyValuePair<string, string>> filters = new BindableCollection<KeyValuePair<string, string>>();
        KeyValuePair<string, string> selectedFilter = new KeyValuePair<string, string>();
        private USCities _tempCities;
        private State selectedState;
        public State SelectedState
        {
            get { return selectedState; }
            set { selectedState = value; NotifyOfPropertyChange(() => SelectedState); }
        }

        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set { selectedCity = value; NotifyOfPropertyChange(() => SelectedCity); }
        }

        private USCities _cities = new USCities();
        public USCities Cities
        {
            get { return _cities; }
            set { _cities = value; NotifyOfPropertyChange(() => Cities); }
        }

        private USStates _states = new USStates();
        public USStates States
        {
            get { return _states; }
            set { _states = value; NotifyOfPropertyChange(() => States); }
        }
        public KeyValuePair<string, string> SelectedFilter
        {
            get { return selectedFilter; }
            set { selectedFilter = value; NotifyOfPropertyChange(() => SelectedFilter); }
        }

        public ObservableCollection<Post> FilteredPosts
        {
            get { return filteredPosts; }
            set { filteredPosts = value; NotifyOfPropertyChange(() => FilteredPosts); }
        }

        public BindableCollection<KeyValuePair<string, string>> Filters
        {
            get { return filters; }
            set { filters = value; NotifyOfPropertyChange(() => Filters); }
        }

        public IUser User
        {
            get { return _user; }
            set { _user = value; NotifyOfPropertyChange(() => User); }
        }

        public Visibility NewPostButtonVisible
        {
            get
            {
                if (User.UserID > 0)
                {
                    return _newPostButtonVisible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set { _newPostButtonVisible = value; NotifyOfPropertyChange(() => NewPostButtonVisible); }
        }

        public Visibility PostButtonVisible
        {
            get
            {
                if (User.UserID > 0)
                {
                    return _postButtonVisible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set { _postButtonVisible = value; NotifyOfPropertyChange(() => PostButtonVisible); }
        }

        public Visibility ReplyButtonVisible
        {
            get
            {
                if (User.UserID > 0 && SelectedPost != null)
                {
                    return _replyButtonVisible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set { _replyButtonVisible = value; NotifyOfPropertyChange(() => ReplyButtonVisible); }
        }

        public Visibility ReplyPostVisible
        {
            get
            {
                if (User.UserID > 0 && SelectedPost != null)
                {
                    return _replyPostVisible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set { _replyPostVisible = value; NotifyOfPropertyChange(() => ReplyPostVisible); }
        }

        public Post ReplyPost
        {
            get { return _replyPost; }
            set { _replyPost = value; NotifyOfPropertyChange(() => ReplyPost); }
        }

        public string ModuleId
        {
            get { return _moduleId; }
            set { _moduleId = value; }
        }

        public IEventAggregator EventAgregator
        {
            get { return _eventAgregator; }
            set { _eventAgregator = value; }
        }

        public IDataLayer DataLayer
        {
            get { return _dataLayer; }
            set { _dataLayer = value; }
        }

        public Visibility OldPostsVisible
        {
            get { return _oldPostsVisible; }
            set { _oldPostsVisible = value; NotifyOfPropertyChange(() => OldPostsVisible); }
        }

        public Visibility NewPostVisible
        {
            get
            {
                if (User.UserID > 0)
                {
                    return _newPostVisible;
                }
                else
                {
                    OldPostsVisible = Visibility.Visible;
                    return Visibility.Collapsed;
                }
            }
            set { _newPostVisible = value; NotifyOfPropertyChange(() => NewPostVisible); }
        }

        public Post NewPost
        {
            get { return _newPost; }
            set { _newPost = value; NotifyOfPropertyChange(() => NewPost); }
        }

        public Post SelectedPost
        {
            get { return _selectedPost; }
            set { _selectedPost = value; NotifyOfPropertyChange(() => SelectedPost); }
        }

        public ObservableCollection<Post> Posts
        {
            get { return _posts; }
            set { _posts = value; NotifyOfPropertyChange(() => Posts); }
        }

        public void Handle(LoginEvent message)
        {
            ShowHideButtons();
            NotifyOfPropertyChange(() => NewPostVisible);
            NotifyOfPropertyChange(() => ReplyPostVisible);
        }

        public void Handle(USEducationGetDataEvent message)
        {
            ObservableCollection<Dictionary<string, string>> result = message.Output;
            Posts.Clear();
            foreach (var item in result)
            {
                Posts.Add(new Post()
                {
                    Id = Convert.ToInt32(item["ID"]),
                    PostedBy = item["UserName"].ToString(),
                    PostDate = item["PostDate"].ToString(),
                    ModuleID = Convert.ToInt32(item["ModuleID"]),
                    PostString = item["PostString"].ToString(),
                    PostSubject = item["PostSubject"].ToString(),
                    FilterID = item["FilterID"].ToString(),
                    CityID = Convert.ToInt32(item["CityID"].ToString()),
                    StateID = Convert.ToInt32(item["StateID"].ToString())
                });
            }
            FilterPosts();
            OldPostsVisible = Visibility.Visible;
            NewPostVisible = Visibility.Collapsed;
            ReplyPostVisible = Visibility.Collapsed;
            ShowHideButtons();
        }

        public void Handle(USEducationGetSubPostsEvent message)
        {
            if (SelectedPost != null)
            {
                SelectedPost.Children.Clear();
                ObservableCollection<Dictionary<string, string>> result = message.Output;
                foreach (var item in result)
                {
                    SelectedPost.Children.Add(new Post()
                    {
                        Id = Convert.ToInt32(item["ID"]),
                        PostedBy = item["UserName"].ToString(),
                        PostDate = item["PostDate"].ToString(),
                        ModuleID = Convert.ToInt32(item["ModuleID"]),
                        PostString = item["PostString"].ToString(),
                        PostSubject = item["PostSubject"].ToString()
                    });
                }
                OldPostsVisible = Visibility.Visible;
                NewPostVisible = Visibility.Collapsed;
                ReplyPostVisible = Visibility.Collapsed;
                ShowHideButtons();
            }
        }

        public void Handle(USEducationInsertMainPostEvent message)
        {
            if (message.IsSuccess == true)
            {
                DataLayer.GetPostsForModule(ModuleId);
                NewPost = null;
                NewPostVisible = Visibility.Collapsed;
                ShowHideButtons();
            }
        }

        public void Handle(USEducationInsertReplyPostEvent message)
        {
            if (message.IsSuccess == true)
            {
                PostSelected();
                ReplyPost = null;
                ReplyPostVisible = Visibility.Collapsed;
                ShowHideButtons();
            }
        }

        public void NewPostClick()
        {
            NewPostVisible = Visibility.Visible;
            OldPostsVisible = Visibility.Collapsed;
            ReplyPostVisible = Visibility.Collapsed;
            ShowHideButtons();
            NewPost = new Post();
            NewPost.PostedBy = User.UserName;
        }

        public void ReplyPostClick()
        {
            ReplyPostVisible = Visibility.Visible;
            NewPostVisible = Visibility.Collapsed;
            ShowHideButtons();
            ReplyPost = new Post();
        }

        public void SaveNewPost()
        {
            if (NewPost != null)
            {
                DataLayer.InsertMainPost(ModuleId, User.UserID.ToString(), NewPost.PostString, NewPost.PostSubject, SelectedFilter.Key, SelectedCity == null ? "0" : SelectedCity.ID.ToString(), SelectedState == null ? "0" : SelectedState.ID.ToString());
            }
            else if (ReplyPost != null)
            {
                DataLayer.InsertReplyPost(ModuleId, SelectedPost.Id.ToString(), User.UserID.ToString(), ReplyPost.PostString);
            }
        }

        public void PostSelected()
        {
            if (SelectedPost != null)
            {
                DataLayer.GetSubPostsForModule(ModuleId, SelectedPost.Id.ToString());
                ShowHideButtons();
            }
        }

        public void CancelNewPost()
        {
            OldPostsVisible = Visibility.Visible;
            ReplyPostVisible = Visibility.Collapsed;
            NewPostVisible = Visibility.Collapsed;
            ShowHideButtons();
            NewPost = null;
            ReplyPost = null;
        }

        private void ShowHideButtons()
        {
            ShowHideReplyPostButton();
            ShowHideNewPostButton();
            ShowHidePostCancelButtons();
        }

        private void ShowHideReplyPostButton()
        {
            if (ReplyPostVisible == Visibility.Collapsed
                && NewPostVisible == Visibility.Collapsed
                && OldPostsVisible == Visibility.Visible
                && SelectedPost != null
                && User.UserID > 0)
            {
                ReplyButtonVisible = Visibility.Visible;
            }
            else
            {
                ReplyButtonVisible = Visibility.Collapsed;
            }
        }

        private void ShowHideNewPostButton()
        {
            if (OldPostsVisible == Visibility.Visible
                && ReplyPostVisible == Visibility.Collapsed
                && NewPostVisible == Visibility.Collapsed
                && User.UserID > 0)
            {
                NewPostButtonVisible = Visibility.Visible;
            }
            else
            {
                NewPostButtonVisible = Visibility.Collapsed;
            }
        }

        private void ShowHidePostCancelButtons()
        {
            if (NewPostVisible == Visibility.Visible || ReplyPostVisible == Visibility.Visible)
            {
                PostButtonVisible = Visibility.Visible;
            }
            else
            {
                PostButtonVisible = Visibility.Collapsed;
            }
        }
        public USEducationViewModel(IDataLayer dataLayer, IEventAggregator eventAgregator, IUser user,
            USCities usCities, USStates states)
        {
            ModuleId = "1";
            DisplayName = "USEducation";
            EventAgregator = eventAgregator;
            EventAgregator.Subscribe(this);
            DataLayer = dataLayer;
            DataLayer.GetPostsForModule(ModuleId);
            DataLayer.GetFiltersByModule(ModuleId);
            User = user;
            _tempCities = usCities;
            this.States.Add(new State() { ID = 0, Name = "All" });
            foreach(State state in states)
            {
                this.States.Add(state);
            }            
            SelectedState = (State)States[0];
            StateChanged();
        }

        public void Handle(USEducationGetFiltersEvent message)
        {
            ObservableCollection<Dictionary<string, string>> result = message.Output;
            Filters.Add(new KeyValuePair<string, string>("0", "All"));
            foreach (var Item in result)
            {
                Filters.Add(new KeyValuePair<string, string>(Item["ID"], Item["Description"]));
            }
            SelectedFilter = (KeyValuePair<string, string>)Filters.Where(P => P.Key == "0").ToList()[0];
        }

        public void FilterPosts()
        {
            FilteredPosts.Clear();
            if (SelectedFilter.Key != null && SelectedFilter.Key != "0")
            {
                FilteredPosts = new ObservableCollection<Post>(Posts.Where(P => P.FilterID == SelectedFilter.Key));
                }
                else
                {
                FilteredPosts = new ObservableCollection<Post>(Posts); 
                }

            if (SelectedState != null && SelectedState.Name != "All")
                {
                FilteredPosts = new ObservableCollection<Post>(FilteredPosts.Where(P => P.StateID == SelectedState.ID));
                }

            if (SelectedCity != null && SelectedCity.CityName != "All")
                    {
                FilteredPosts = new ObservableCollection<Post>(FilteredPosts.Where(P => P.CityID == SelectedCity.ID));
            }

            //if (SelectedFilter.Key != null && SelectedFilter.Key != "0")
            //{
            //    if (SelectedCity != null)
            //    {
            //        if (SelectedCity.ID != 0)
            //        {
            //            FilteredPosts = new ObservableCollection<Post>(Posts.Where(P => P.FilterID == SelectedFilter.Key && P.CityID == SelectedCity.ID));
            //        }
            //        else if (SelectedState.Name != "All")
            //        {
            //            FilteredPosts.Clear();
            //            foreach (City city in Cities.Where(P => P.ID != 0))
            //            {
            //                foreach (Post post in Posts.Where(P => P.FilterID == SelectedFilter.Key && P.CityID == city.ID).ToList<Post>())
            //                {
            //                    FilteredPosts.Add(post);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        FilteredPosts = new ObservableCollection<Post>(Posts.Where(P => P.FilterID == SelectedFilter.Key));
            //    }
            //}
            //else if (SelectedCity != null)
            //{
            //    if (SelectedCity.ID != 0)
            //    {
            //        FilteredPosts = new ObservableCollection<Post>(Posts.Where(P => P.CityID == SelectedCity.ID));
            //    }
            //    else if (SelectedState.Name != "All")
            //    {
            //        FilteredPosts.Clear();
            //        foreach (City city in Cities.Where(P => P.ID != 0))
            //        {
            //            foreach (Post post in Posts.Where(P => P.CityID == city.ID).ToList<Post>())
            //            {
            //                FilteredPosts.Add(post);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    FilteredPosts = new ObservableCollection<Post>(Posts);
            //}
            NotifyOfPropertyChange(() => ReplyPostVisible);
            ShowHideButtons();
        }

        public void StateChanged()
        {
            Cities.Clear();
            if (SelectedState != null && SelectedState.Name != "All")
            {
                Cities.Add(new City() { ID = 0, CityName = "All", State = SelectedState.Name, StateID = SelectedState.ID });
                foreach (City city in _tempCities.Where(P => P.State == SelectedState.Name).ToList<City>())
                {
                    Cities.Add(city);
                }
                SelectedCity = (City)Cities[0];
            }
        }

        public void CityChanged()
        {
            FilterPosts();
        }
    }

    public interface IUSEducationViewModel:IScreen
    { }
}
