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
    /// SupplierView.xaml 的交互逻辑
    /// </summary>
    public partial class SupplierView : UserControl
    {
        //生命周期与 View 相同
        private SupplierViewModel vm = new SupplierViewModel();
        public SupplierView()
        {
            InitializeComponent();
            //上下文
            DataContext = vm;
        }
    }
}
