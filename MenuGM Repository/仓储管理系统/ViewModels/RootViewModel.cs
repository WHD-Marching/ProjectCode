using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;
using 仓储管理系统.Views;

namespace 仓储管理系统.ViewModels
{
    public partial class RootViewModel : ObservableObject
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IUserMenuRepository _userMenuRepository;

        /// <summary>
        ///  可见性补充
        ///  1. SelectedMember 变更时刷新，数据库
        ///  2. HasUserMenus 属性正确通知界面
        ///  3. XAML中菜单可见性,绑定到 HasUserMenus 或 SelectedMember 属性.
        /// </summary>

        #region  属性
        private Member _selectedMember;
        public Member SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                if (_selectedMember != value)
                {
                    _selectedMember = value;
                    OnPropertyChanged(nameof(SelectedMember));
                    RefreshUserMenus();
                    OnPropertyChanged(nameof(HasUserMenus));
                }
            }
        }

        private string _searchName = "";

        public string SearchName
        {
            get { return _searchName; }
            set
            {
                if (_searchName != value)
                {
                    _searchName = value;
                    OnPropertyChanged(nameof(SearchName));
                }
            }
        }

        private ObservableCollection<Member> _members = new ObservableCollection<Member>();
        public ObservableCollection<Member> Members
        {
            get { return _members; }
            set
            {
                if (_members != value)
                {
                    _members = value;
                    OnPropertyChanged(nameof(Members));
                }
            }
        }

        private ObservableCollection<UserMenuDisplay> _userMenus = new ObservableCollection<UserMenuDisplay>();
        public ObservableCollection<UserMenuDisplay> UserMenus
        {
            get { return _userMenus; }
            private set
            {
                if (_userMenus != value)
                {
                    _userMenus = value;
                    OnPropertyChanged(nameof(UserMenus));
                    OnPropertyChanged(nameof(HasUserMenus));
                }
            }
        }


        /// <summary>
        /// 可见性要求 2
        /// </summary>
        public bool HasUserMenus
        {
            get { return UserMenus != null && UserMenus.Any(); }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }

        #endregion


        public ICommand SearchCommand { get; }
        public ICommand ManagePermissionsCommand { get; }
        public ICommand OpenAddMenuDialogCommand { get; }
        public ICommand DeleteMenuPermissionCommand { get; }

        public RootViewModel(
            IMemberRepository memberRepository,
            IMenuRepository menuRepository,
            IUserMenuRepository userMenuRepository)
        {
            //null 检查
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(menuRepository));
            _userMenuRepository = userMenuRepository ?? throw new ArgumentNullException(nameof(userMenuRepository));

            SearchCommand = new RelayCommand(SearchMembers);
            ManagePermissionsCommand = new RelayCommand<Member>(ManagePermissions);
            OpenAddMenuDialogCommand = new RelayCommand(OpenAddMenuDialog, () => SelectedMember != null);
            DeleteMenuPermissionCommand = new RelayCommand<int>(DeleteMenuPermission);

            // 初始化成员列表
            SearchMembers();
        }

        /// <summary>
        ///  用户查询
        /// </summary>
        private void SearchMembers()
        {
            // 从仓储获取所有用户
            var allMembers = _memberRepository.GetAll() ?? new List<Member>();
            // 根据SearchName过滤
            var filtered = string.IsNullOrWhiteSpace(SearchName)
                ? allMembers
                : allMembers.Where(m => m.Name != null && m.Name.Contains(SearchName)).ToList();
            Members = new ObservableCollection<Member>(filtered);
        }

        private void ManagePermissions(Member member)
        {
            if (member == null) return;
            SelectedMember = member;
        }

        /// <summary>
        /// 分配菜单--对话框
        /// </summary>
        private void OpenAddMenuDialog()
        {
            if (SelectedMember == null)
            {
                StatusMessage = "请先选择用户";
                return;
            }

            // 获取所有菜单
            var allMenus = _menuRepository.GetAll()?.ToList() ?? new List<Menu>();
            // 获取当前用户已分配的菜单ID
            var currentMenuIds = _userMenuRepository.GetByUserId(SelectedMember.Id).Select(um => um.MenuId).ToList();

            // 创建菜单选择对话框的ViewModel
            var dialogVm = new MenuDialogViewModel(allMenus, currentMenuIds);

            // 创建并显示对话框
            var dialog = new MenuDialogView(allMenus, currentMenuIds)
            {
                DataContext = dialogVm,
                Owner = System.Windows.Application.Current.MainWindow
            };
            var result = dialog.ShowDialog();

            // 如果用户点击确定
            if (result == true)
            {
                // 获取用户选择的菜单ID
                var selectedMenuIds = dialogVm.SelectedMenuIds;

                // 先删除该用户所有旧的菜单权限
                var oldMenuIds = currentMenuIds;
                foreach (var menuId in oldMenuIds)
                {
                    _userMenuRepository.Remove(SelectedMember.Id, menuId);
                }

                // 添加新选择的菜单权限
                foreach (var menuId in selectedMenuIds)
                {
                    _userMenuRepository.Add(new UserMenu
                    {
                        UserId = SelectedMember.Id,
                        MenuId = menuId,
                        CreatedAt = DateTime.Now
                    });
                }

                StatusMessage = "菜单权限已更新";
                RefreshUserMenus();
                OnPropertyChanged(nameof(HasUserMenus));
            }
            else
            {
                StatusMessage = "已取消菜单分配";
            }
        }

        public void DeleteMenuPermission(int menuId)
        {
            if (SelectedMember != null)
            {
                _userMenuRepository.Remove(SelectedMember.Id, menuId);
                StatusMessage = "菜单权限已删除";
                RefreshUserMenus();
                OnPropertyChanged(nameof(HasUserMenus));
            }
        }

        /// <summary>
        ///  可见性 2
        /// </summary>
        private void RefreshUserMenus()
        {
            var displays = GetUserMenuDisplays(SelectedMember != null ? SelectedMember.Id : 0);
            UserMenus = new ObservableCollection<UserMenuDisplay>(displays);
        }

        private IEnumerable<UserMenuDisplay> GetUserMenuDisplays(int userId)
        {
            if (userId <= 0) yield break;

            //null检查
            var userMenus = _userMenuRepository?.GetByUserId(userId) ?? Enumerable.Empty<UserMenu>();
            var allMenus = _menuRepository?.GetAll() ?? Enumerable.Empty<Menu>();

            foreach (var userMenu in userMenus)
            {
                var menu = allMenus.FirstOrDefault(m => m.MenuId == userMenu.MenuId);
                if (menu != null)
                {
                    yield return new UserMenuDisplay
                    {
                        MenuId = menu.MenuId,
                        MenuName = menu.MenuName,
                        MenuKey = menu.MenuKey,
                        CreatedAt = userMenu.CreatedAt
                    };
                }
            }
        }

        
        // 用于显示的权限模型
        public class UserMenuDisplay
        {
            public int MenuId { get; set; }
            public string MenuName { get; set; }
            public string MenuKey { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        // 简单 RelayCommand实现
        public class RelayCommand : ICommand
        {
            private readonly Action _execute; //是否可执行的委托
            private readonly Func<bool> _canExecute; // 保存判断命令是否可执行的委托
            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }
            public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
            public void Execute(object parameter) => _execute();
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }

        public class RelayCommand<T> : ICommand
        {
            private readonly Action<T> _execute;
            private readonly Func<T, bool> _canExecute;
            public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }
            public bool CanExecute(object parameter)
            {
                if (_canExecute == null) return true;
                //T为值类型，返回默认值
                if (parameter == null && typeof(T).IsValueType) return _canExecute(default(T));
                return _canExecute((T)parameter);
            }
            public void Execute(object parameter)
            {
                if (parameter == null && typeof(T).IsValueType)
                    _execute(default(T));
                else
                    _execute((T)parameter);
            }
            // 订阅和取消订阅命令可执行状态变化事件
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}
