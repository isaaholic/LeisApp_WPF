﻿using Source.Commands;
using Source.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Source.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand GoProfileCommand { get; }

        public MainViewModel(NavigationStore navigationStore)
        {
            GoProfileCommand = new NavigateCommand(navigationStore);
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModel = new HomeViewModel();
            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentViewModelChanged;
        }

        private void _navigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}