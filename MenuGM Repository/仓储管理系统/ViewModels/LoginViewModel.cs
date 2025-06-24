using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;
using 仓储管理系统.Views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace 仓储管理系统.ViewModels
{
    internal class LoginViewModel
    {
        public LoginViewModel()
        {
            //测试
   #if DEBUG
           Member.Name = "admin";
           Member.Password = "WHD-Marching";
   #endif
            //中断命令 RelayCommand 
            LoginCommand = new RelayCommand(OnLoginCommand);
            ExitCommand = new RelayCommand(OnExitCommand);
        }

        //初始化,给到view界面的Text文本---即同步账号名
        public Member Member { get; set; } = new Member();

        //ICommand命令 {get;set;}
        public ICommand LoginCommand { get; }
        public ICommand ExitCommand { get; }

        private void OnExitCommand()
        {
            //主窗口资源手动关闭
            //System.Windows.Application.Current.MainWindow.Close();
            // 全局
            AppData.MainWindow?.Close();
        }

        private void OnLoginCommand()
        {
            //空值判断
            if (string.IsNullOrEmpty(Member.Name))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }

            if (string.IsNullOrEmpty(Member.Password))
            {
                MessageBox.Show("密码不能为空");
                return;
            }

            //实例--用户仓库表
            var memberRepository = new MemberRepository();

            //返回列表第一个满足条件的元素
            var member = memberRepository.GetAll().Find(p => p.Name == Member.Name && p.Password == Member.Password);
            // 若返回null,错误
            if (member == null)
            {
                MessageBox.Show("用户名或密码错误!");
            }
            else
            {
                MessageBox.Show("恭喜你,登录世界!");

                // member给到全局类
                AppData.CurrentUser = member;

                // @ 更新可见菜单
                // 创建主界面并设置DataContext
                var mainView = new MainView();
                // 获取MainViewModel并设置当前用户
                if (mainView.DataContext is MainViewModel mainViewModel)
                {
                    mainViewModel.CurrentUser = member;
                }

                // 窗口界面转场
                // 显示同一个实例，而不是 new MainView();
                AppData.Container.Content = mainView;

                AppData.MainWindow.Width = 1300;
                AppData.MainWindow.Height = 600;
                //显示器计算
                AppData.MainWindow.Left = (SystemParameters.WorkArea.Width - AppData.MainWindow.Width) / 2;
                AppData.MainWindow.Top = (SystemParameters.WorkArea.Height - AppData.MainWindow.Height) / 2;
            }
        }
    }
}