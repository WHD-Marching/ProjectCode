using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;
using 仓储管理系统.Views;

namespace 仓储管理系统.ViewModels
{
    internal class LoginViewModel
    {
        //初始化,给到view界面的Text文本---即同步账号名
        public Member Member { get; set; } = new Member();

        //ICommand命令 {get;set;}
        public ICommand LoginCommand { get; }
        public ICommand ExitCommand { get; }

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
            if (String.IsNullOrEmpty(Member.Name))
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
            MemberRepository memberRepository = new MemberRepository();

            //返回列表第一个满足条件的元素
            var member = memberRepository.GetAll().Find(p => p.Name == Member.Name && p.Password == Member.Password);
            // 若返回null,错误
            if (member == null)
            {
                MessageBox.Show("用户名或密码错误!");
                return;
            }
            else
            {
                MessageBox.Show("恭喜你,登录世界!");

                // member给到全局类
                AppData.CurrentUser = member;
                // 窗口界面转场(将MainView设置为容器的内容)
                AppData.Container.Content = new MainView();

                AppData.MainWindow.Width = 1300;
                AppData.MainWindow.Height = 600;
                //显示器计算
                AppData.MainWindow.Left = (SystemParameters.WorkArea.Width - AppData.MainWindow.Width) / 2;
                AppData.MainWindow.Top = (SystemParameters.WorkArea.Height - AppData.MainWindow.Height) / 2;
            }
        }


    }



}
