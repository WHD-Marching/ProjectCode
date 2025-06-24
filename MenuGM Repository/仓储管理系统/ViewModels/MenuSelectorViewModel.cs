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
using 仓储管理系统.Entities;

namespace 仓储管理系统.ViewModels
{
    //菜单选择对话框
    public class MenuSelectorViewModel : ObservableObject
    {
        public ObservableCollection<MenuTreeItem> MenuTree { get; } = new ObservableCollection<MenuTreeItem>();
        public List<int> SelectedMenuIds { get; private set; } = new List<int>();

        public ICommand ConfirmCommand { get; }

        public MenuSelectorViewModel(IEnumerable<Menu> menus, List<int> currentSelection)
        {
            BuildMenuTree(menus, currentSelection);
            ConfirmCommand = new RelayCommand(Confirm);
        }

        private void BuildMenuTree(IEnumerable<Menu> menus, List<int> currentSelection)
        {
            var rootMenus = menus.Where(m => m.ParentMenuId == null)
                                 .OrderBy(m => m.DisplayOrder)
                                 .ToList();

            foreach (var menu in rootMenus)
            {
                var treeItem = new MenuTreeItem(menu, currentSelection);
                AddChildren(treeItem, menus, currentSelection);
                MenuTree.Add(treeItem);
            }
        }

        private void AddChildren(MenuTreeItem parent, IEnumerable<Menu> allMenus, List<int> currentSelection)
        {
            var children = allMenus.Where(m => m.ParentMenuId == parent.MenuId)
                                   .OrderBy(m => m.DisplayOrder)
                                   .ToList();

            foreach (var menu in children)
            {
                var treeItem = new MenuTreeItem(menu, currentSelection);
                parent.Children.Add(treeItem);
                AddChildren(treeItem, allMenus, currentSelection);
            }
        }

        private void Confirm()
        {
            SelectedMenuIds = CollectSelectedIds(MenuTree);
            ((Window)this).DialogResult = true;
        }

        public static explicit operator Window(MenuSelectorViewModel v)
        {
            throw new NotImplementedException();
        }

        private List<int> CollectSelectedIds(IEnumerable<MenuTreeItem> items)
        {
            var ids = new List<int>();

            foreach (var item in items)
            {
                if (item.IsSelected == true)
                {
                    ids.Add(item.MenuId);
                }

                ids.AddRange(CollectSelectedIds(item.Children));
            }

            return ids;
        }
    }

    public class MenuTreeItem : ObservableObject
    {
        public int MenuId { get; }
        public string MenuName { get; }
        public ObservableCollection<MenuTreeItem> Children { get; } = new ObservableCollection<MenuTreeItem>();

        private bool? _isSelected;
        public bool? IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public MenuTreeItem(Menu menu, List<int> currentSelection)
        {
            MenuId = menu.MenuId;
            MenuName = menu.MenuName;
            IsSelected = currentSelection.Contains(menu.MenuId);
        }
    }
}
