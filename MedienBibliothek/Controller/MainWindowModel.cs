﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Input;
using MedienBibliothek.Model;


namespace MedienBibliothek.Controller
{
    class MainWindowModel : INotifyPropertyChanged
    {
        DirectoryInfo videoPath = new DirectoryInfo(@"c:\Movies");
        public MainWindowModel()
        {
            RefreshButtonName = "Refresh video list";
            AddVideoToListView = new CollectionView(GetAvailableVideos());
        }

        private string _refreshButtonName;
        public string RefreshButtonName
        {
            get
            {
                return _refreshButtonName;
            }
            set
            {
                _refreshButtonName = value;
                OnPropertyChanged("RefreshButtonName");
            }
        }

        private CollectionView _addVideoToListView;
        public CollectionView AddVideoToListView
        {
            get
            {
                return _addVideoToListView;
            }
            set
            {
                _addVideoToListView = value;
                OnPropertyChanged("AddVideoTiListView");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        
        private DelegateCommand _refreshVideoListCommand;
        public ICommand RefreshVideoListCommand
        {
            get
            {
                if (_refreshVideoListCommand == null)
                {
                    _refreshVideoListCommand = new DelegateCommand(CheckForNewVideos);
                }
                return _refreshVideoListCommand;
            }
        }



//        private void RefreshVideoList()
//        {
//            RefreshButtonName = "Refreshing...";
//            string[] testArray = Directory.GetFiles(videoPath, "*.mkv");
//            AddVideoToListView = testArray[0];
//            RefreshButtonName = testArray[0];
//
//        }

        private void CheckForNewVideos()
        {
            GetAvailableVideos();
        }

        private List<DirectoryInfo> GetAvailableVideos()
        {
            var listOfVideos = new List<DirectoryInfo>();
            if(videoPath.Exists)
            {
                FileInfo[] videoFiles = videoPath.GetFiles("*.mkv", SearchOption.AllDirectories);
                foreach (var videoFile in videoFiles)
                {
                    if(videoFile.Length >= 100000000)
                    {
                        listOfVideos.Add(new DirectoryInfo(SplitVideoFolderName(videoFile.DirectoryName)));
                    }
                    
                }
            }

            return listOfVideos;
        } 

        private string SplitVideoFolderName(string fullDirectoryPath)
        {
            string[] videoName = fullDirectoryPath.Split('\\');
//            string[] videoName = fullDirectoryPath.Split(videoPath, StringSplitOptions.RemoveEmptyEntries)
            return videoName[2];
        }
    
    
    
    }
}
