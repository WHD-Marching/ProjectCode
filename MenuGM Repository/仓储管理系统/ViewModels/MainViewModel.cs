using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;
using 仓储管理系统.Views;

namespace 仓储管理系统.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUserMenuRepository _userMenuRepository;

        // 当前登录用户
        private Member _currentUser;
        public Member CurrentUser
        {
            get => _currentUser;
            set
            {
                SetProperty(ref _currentUser, value);
                UpdateVisibleMenus();
            }
        }
        // 可见菜单集合
        private ObservableCollection<Menu> _visibleMenus = new ObservableCollection<Menu>();
        public ObservableCollection<Menu> VisibleMenus
        {
            get => _visibleMenus;
            set => SetProperty(ref _visibleMenus, value);
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

       
        public ICommand NavigationCommand { get; }
        public MainViewModel(IMenuRepository menuRepository, IUserMenuRepository userMenuRepository)
        {
            _menuRepository = menuRepository;
            _userMenuRepository = userMenuRepository;

            CurrentView = new IndexView();//默认视图
            NavigationCommand = new RelayCommand<string>(OnNavigationCommand);
        }


        /// <summary>
        ///  登录调用,刷新可见菜单
        /// </summary>
        public void UpdateVisibleMenus()
        {
            VisibleMenus.Clear();
            if (CurrentUser == null) return;

            var allMenus = _menuRepository.GetAll();
            var userMenuIds = _userMenuRepository.GetByUserId(CurrentUser.Id).Select(um => um.MenuId).ToHashSet();

            foreach (var menu in allMenus)
            {
                if (userMenuIds.Contains(menu.MenuId))
                    VisibleMenus.Add(menu);
            }
        }

        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="viewName"></param>
        private void OnNavigationCommand(string viewName)
        {
            try
            {
                // 获取当前程序集名
                var assemblyName = typeof(MainViewModel).Assembly.GetName().Name;
                // 拼接完整类型名
                var typeName = $"仓储管理系统.Views.{viewName}";
                var type = Type.GetType(typeName);
                if (type == null)
                {
                    MessageBox.Show($"未找到视图类型: {typeName}");
                    return;
                }

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
