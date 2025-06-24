using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.Entities;

namespace 仓储管理系统.ViewModels
{
    public  class MenuDialogViewModel:ObservableObject
    {
        public ObservableCollection<MenuTreeItem> MenuTree { get; private set; }
        public List<int> SelectedMenuIds { get; private set; } = new List<int>();
        public ICommand ConfirmCommand { get; private set; }


        public MenuDialogViewModel(IEnumerable<Menu> allMenus, List<int> currentSelection)
        {
            BuildMenuTree(allMenus, currentSelection);
            ConfirmCommand = new RelayCommand(Confirm);
        }

        private void BuildMenuTree(IEnumerable<Menu> menus, List<int> currentSelection)
        {
            var rootMenus = menus.Where(m => m.ParentMenuId == null).ToList();
            var tree = new ObservableCollection<MenuTreeItem>();
            foreach (var menu in rootMenus)
            {
                var item = new MenuTreeItem(menu, currentSelection);
                AddChildren(item, menus, currentSelection);
                tree.Add(item);
            }
            MenuTree = tree;
            OnPropertyChanged(nameof(MenuTree));
        }
        private void AddChildren(MenuTreeItem parent, IEnumerable<Menu> allMenus, List<int> currentSelection)
        {
            var children = allMenus.Where(m => m.ParentMenuId == parent.MenuId).ToList();
            foreach (var menu in children)
            {
                var item = new MenuTreeItem(menu, currentSelection);
                parent.Children.Add(item);
                AddChildren(item, allMenus, currentSelection);
            }
        }

        /// <summary>
        ///  对话框响应处理
        /// </summary>
        private void Confirm()
        {
            //递归收集所有被选中的菜单ID
            SelectedMenuIds = CollectSelectedIds(MenuTree);
        }
        private List<int> CollectSelectedIds(IEnumerable<MenuTreeItem> items)
        {
            var ids = new List<int>();
            foreach (var item in items)
            {
                // 如果当前项被选中，则添加到ID列表
                if (item.IsSelected == true)
                {
                    ids.Add(item.MenuId);
                }
                ids.AddRange(CollectSelectedIds(item.Children));
            }
            return ids;
        }

        
        public class MenuTreeItem : INotifyPropertyChanged
        {
            public int MenuId { get; }
            public string MenuName { get; }
            public ObservableCollection<MenuTreeItem> Children { get; } = new ObservableCollection<MenuTreeItem>();
            private bool? _isSelected;
            public bool? IsSelected
            {
                get { return _isSelected; }
                set
                {
                    if (_isSelected != value)
                    {
                        _isSelected = value;
                        OnPropertyChanged(nameof(IsSelected));
                    }
                }
            }

            public MenuTreeItem(Menu menu, List<int> currentSelection)
            {
                MenuId = menu.MenuId;
                MenuName = menu.MenuName;
                IsSelected = currentSelection.Contains(menu.MenuId);
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            // 简单RelayCommand实现
            
        }

    }


}


