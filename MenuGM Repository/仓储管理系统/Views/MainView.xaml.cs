using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using 仓储管理系统.Entities;
using 仓储管理系统.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace 仓储管理系统.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        private MainViewModel vm ;
        public MainView()
        {
            InitializeComponent();

            //在xaml中做 mainRegon
            AppData.MainRegion = mainRegion;
             
            // 通过依赖注入获取 ViewModel
            vm = App.ServiceProvider.GetRequiredService<MainViewModel>();
            DataContext = vm;

        }
    }
}
