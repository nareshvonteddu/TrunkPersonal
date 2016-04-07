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
using NRIUturn.CustomControls;

namespace NRIUturn.ViewModels
{
    public class ImigrationViewModel : Screen, INotifyPropertyChangedEx, IImigrationViewModel, IHandle<ImigrationGetDataEvent>, IHandle<ImigrationGetSubPostsEvent>, IHandle<ImigrationInsertMainPostEvent>, IHandle<ImigrationInsertReplyPostEvent>, IHandle<LoginEvent>, IHandle<ImigrationGetFiltersEvent>
    {
        ListBox list;
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


        public ImigrationViewModel(IDataLayer dataLayer, IEventAggregator eventAgregator, IUser user)
        {
            ModuleId = "7";
            DisplayName = "Imigration";
            EventAgregator = eventAgregator;
            EventAgregator.Subscribe(this);
            DataLayer = dataLayer;
            DataLayer.GetPostsForModule(ModuleId);
            DataLayer.GetFiltersByModule(ModuleId);
            User = user;
        }

        public void Handle(LoginEvent message)
        {
            ShowHideButtons();
            NotifyOfPropertyChange(() => NewPostVisible);
            NotifyOfPropertyChange(() => ReplyPostVisible);
        }

        public void Handle(ImigrationGetDataEvent message)
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
                    FilterID = item["FilterID"].ToString()
                });
            }
            FilterPosts();
            OldPostsVisible = Visibility.Visible;
            NewPostVisible = Visibility.Collapsed;
            ReplyPostVisible = Visibility.Collapsed;
            ShowHideButtons();
        }

        public void Handle(ImigrationGetSubPostsEvent message)
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

        public void Handle(ImigrationInsertMainPostEvent message)
        {
            if (message.IsSuccess == true)
            {
                DataLayer.GetPostsForModule(ModuleId);
                NewPost = null;
                NewPostVisible = Visibility.Collapsed;
                ShowHideButtons();
            }
        }

        public void Handle(ImigrationInsertReplyPostEvent message)
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

        public void SaveNewPost(SilverlightTextEditor ctrl)
        {
            if (NewPost != null)
            {
                DataLayer.InsertMainPost(ModuleId, User.UserID.ToString(), ctrl.Xaml, NewPost.PostSubject, SelectedFilter.Key);
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

        public void Handle(ImigrationGetFiltersEvent message)
        {
            ObservableCollection<Dictionary<string, string>> result = message.Output;
            foreach (var Item in result)
            {
                Filters.Add(new KeyValuePair<string, string>(Item["ID"], Item["Description"]));
            }            
            SelectedFilter = (KeyValuePair<string, string>)Filters.Where(P => P.Key == "1").ToList()[0];
        }

        public void FilterPosts()
        {            
            if (SelectedFilter.Key != null)
            {
                FilteredPosts = new ObservableCollection<Post>(Posts.Where(P => P.FilterID == SelectedFilter.Key));
            }
            else
            {
                FilteredPosts = Posts;
            }
            NotifyOfPropertyChange(() => ReplyPostVisible);
            ShowHideButtons();
        }
    }

    public interface IImigrationViewModel:IScreen
    { }
}
