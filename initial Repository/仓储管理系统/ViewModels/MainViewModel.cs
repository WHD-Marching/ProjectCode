using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.Views;

namespace 仓储管理系统.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand NavigationCommand { get; }
        public MainViewModel()
        {
            CurrentView = new IndexView();//默认视图
            NavigationCommand = new RelayCommand<string>(OnNavigationCommand);
        }

        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="viewName"></param>
        private void OnNavigationCommand(string viewName)
        {
            try
            {
                var type = Type.GetType("仓储管理系统.Views." + viewName);
                object view = Activator.CreateInstance(type);
                AppData.MainRegion.Content = view;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
