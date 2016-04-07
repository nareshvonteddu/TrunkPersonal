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
using Caliburn.Micro;
using NRIUturn.DataModels;

namespace NRIUturn.ViewModels
{
    public class ForumViewModel:Screen, IHandle<GetDataEvent>, IHandle<GetSubPostsEvent>, IHandle<InsertMainPostEvent>
    {
        IEventAggregator _eventAgegator;
        DataLayer _dataLayer;
        ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        Post _selectedPost;
        Post _newPost = new Post();
        Visibility _newPostVisible = Visibility.Collapsed;
        Visibility _oldPostsVisible = Visibility.Visible;

        public Visibility OldPostsVisible
        {
            get { return _oldPostsVisible; }
            set { _oldPostsVisible = value; NotifyOfPropertyChange(() => OldPostsVisible); }
        }

        public Visibility NewPostVisible
        {
            get { return _newPostVisible; }
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

        public ForumViewModel(DataLayer dataLayer, IEventAggregator eventAgregator)
        {
            DisplayName = "Forum";
            _eventAgegator = eventAgregator;
            _eventAgegator.Subscribe(this);
            _dataLayer = dataLayer;
            _dataLayer.GetPostsForModule("1");
        }

        public void Handle(GetDataEvent message)
        {
            ObservableCollection<Dictionary<string,string>> result = message.Output;
            Posts.Clear();
            foreach (var item in result)
            {
                Posts.Add(new Post()
                {
                    Id = Convert.ToInt32(item["ID"]),
                    PostedBy = item["POSTED_BY"].ToString(),
                    PostDate = item["POST_DATE"].ToString(),
                    ModuleID = Convert.ToInt32(item["MODULE_ID"]),
                    PostString = item["POST_STRING"].ToString(),
                    PostSubject = item["POST_SUBJECT"].ToString()
                });
            }
            NewPostVisible = Visibility.Collapsed;
            OldPostsVisible = Visibility.Visible;
        }

        public void Handle(GetSubPostsEvent message)
        {
            SelectedPost.Children.Clear();
            ObservableCollection<Dictionary<string, string>> result = message.Output;
            foreach (var item in result)
            {
                SelectedPost.Children.Add(new Post()
                {
                    Id = Convert.ToInt32(item["ID"]),
                    PostedBy = item["POSTED_BY"].ToString(),
                    PostDate = item["POST_DATE"].ToString(),
                    ModuleID = Convert.ToInt32(item["MODULE_ID"]),
                    PostString = item["POST_STRING"].ToString(),
                    PostSubject = item["POST_SUBJECT"].ToString()
                });
            }
        }

        public void Handle(InsertMainPostEvent message)
        {
            if (message.IsSuccess == true)
            {
                _dataLayer.GetPostsForModule("1");
                NewPost = new Post();
            }
        }

        public void NewPostClick()
        {
            NewPostVisible = Visibility.Visible;
            OldPostsVisible = Visibility.Collapsed;
        }

        public void SaveNewPost()
        {
            if (NewPost != null)
            {
                _dataLayer.InsertMainPost("1", NewPost.PostedBy, NewPost.PostString, NewPost.PostSubject);
            }
        }

        public void PostSelected()
        {
            if (SelectedPost != null)
            {
                _dataLayer.GetSubPostsForModule("1", SelectedPost.Id.ToString());
            }
        }
    }
}
