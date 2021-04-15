﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestureDrawingTimer.models;

namespace GestureDrawingTimer.viewmodels
{
    public class SessionViewModel : BaseINPC
    {
        // Properties
        private int _currentImageIndex;
        // TODO Session doesn't need this property...
        public int CurrentImageIndex
        {
            get { return _currentImageIndex; }
            private set
            {
                if (value == _currentImageIndex) return;
                _currentImageIndex = value;
                OnPropertyChanged();
            }
        }
        private string _currentImagePath;
        public string CurrentImagePath
        {
            get { return _currentImagePath; }
            private set
            {
                if (value.Equals(_currentImagePath)) return;
                _currentImagePath = value;
                OnPropertyChanged();
            }
        }

        // Instance variables
        private Session mSession;
        private List<string> mShuffledImagePaths;

        // Constructor
        public SessionViewModel(Session session)
        {
            mSession = session;

            // Shuffle Session's list of image paths
            Random randy = new Random();
            mShuffledImagePaths = mSession.ImagePaths.OrderBy(path => randy.Next()).ToList();

            // Display first image
            CurrentImageIndex = 0;
            CurrentImagePath = mShuffledImagePaths[CurrentImageIndex];

            // TODO start an interval timer
        }
    }
}
