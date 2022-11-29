﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Source.Models;
using Source.Commands;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System;

namespace Source.ViewModels
{

    class HomeViewModel : ViewModelBase
    {
        private string _content;

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
        public static ObservableCollection<Tweet>? Tweets { get; set; }
        public ICommand SetImageCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public User? User { get; set; }
        public List<Tweet>? UserTweets { get; set; }
        public List<User>? AllUsers { get; set; }



        private Tweet? _tweet;

        public Tweet? SelectedTweet
        {
            get { return _tweet; }
            set
            {
                _tweet = value;
                OnPropertyChanged(nameof(SelectedTweet));
            }
        }

        private string? _imagePath;

        public string? ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public HomeViewModel()
        {
            User = MainViewModel.User;
            AllUsers = MainViewModel.AllUsers;
            var exUser = new User();

            if (User != null)
            {
                foreach (var item in User.Following)
                {
                    exUser = AllUsers.Find(u => u.Id == item.Id);
                    if (exUser != null)
                    {
                        foreach (var tweet in exUser.Tweets)
                        {
                            UserTweets.Add(tweet);
                        }
                    }
                }
                Tweets = new();
                for (int i = 0; i < UserTweets?.Count; i++)
                {
                    Tweets.Add(UserTweets[i]);
                    SelectedTweet = Tweets[i];
                }

                ImagePath = string.Empty;
            }
            AddCommand = new RelayCommand(AddExecuteCommand, AddCanExecuteCommand);
        }

        private bool AddCanExecuteCommand(object? obj)
        {
            return true;
        }

        private void AddExecuteCommand(object? obj)
        {
            Tweets?.Insert(0, new Tweet()
            {
                Content = Content,
                Created = DateTime.Now,
                User = User,
            });
            Content = String.Empty;

        }

        bool CanExecuteCommand(object? parametr) => true;

        void ExecuteShowCommand(object? parametr)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "img files (*.img)|*.png|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == true)
            {

                string filePath = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                StreamReader sr = new StreamReader(filePath);
                ImagePath = filePath;
            }
        }
    }
}
