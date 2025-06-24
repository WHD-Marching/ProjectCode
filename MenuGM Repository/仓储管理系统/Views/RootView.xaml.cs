using System;
using System.Collections.Generic;
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
using Microsoft.Extensions.DependencyInjection;
using 仓储管理系统.DAL;
using 仓储管理系统.ViewModels;

namespace 仓储管理系统.Views
{
    /// <summary>
    /// RootView.xaml 的交互逻辑
    /// </summary>
    public partial class RootView : UserControl
    {
        private readonly RootViewModel vm;

        //直接用容器解析，而不是手动获取
        public RootView()
        {
            InitializeComponent();

            var serviceProvider = App.ServiceProvider;

            // 直接用容器解析 ViewModel
            vm = serviceProvider.GetRequiredService<RootViewModel>();
            DataContext = vm;
        }

    }
}