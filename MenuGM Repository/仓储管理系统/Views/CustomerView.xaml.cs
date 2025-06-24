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
using 仓储管理系统.ViewModels;

namespace 仓储管理系统.Views
{
    /// <summary>
    /// CustomerView.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerView : UserControl
    {
       // ViewModel 实例作为字段存在，生命周期与 View 相同，可被多个方法共享。
       //跨方法复用
        private CustomerViewModel vm = new CustomerViewModel();
        //顾客管理
        public CustomerView()
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
