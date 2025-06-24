using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 仓储管理系统.DAL;
using 仓储管理系统.Views;

namespace 仓储管理系统
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            // 当前窗口实例存储到全局
            AppData.MainWindow = this;
            AppData.Container = this.container;
            // MainRegion 在MainView()
        }

        //登录界面进入MainView
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 免登录
            // UserControl view = new LoginView();
            // view = new MainView();

            container.Content = new LoginView();
#if DEBUG 
            AppData.MainWindow.Width = 800;
            AppData.MainWindow.Height = 600; 
            AppData.MainWindow.Left = (SystemParameters.WorkArea.Width - AppData.MainWindow.Width) / 2;
            AppData.MainWindow.Top = (SystemParameters.WorkArea.Height - AppData.MainWindow.Height) / 2;
            //查询
            MemberRepository memberRepository = new MemberRepository();
            var member = memberRepository.GetAll().Find(p => p.Name == "admin");
            AppData.CurrentUser = member;
#endif
            //container.Content = view;
        }
    }
}
