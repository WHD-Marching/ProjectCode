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
    /// OutstoreView.xaml 的交互逻辑
    /// </summary>
    public partial class OutstoreView : UserControl
    {
        private OutstoreViewModel vm = new OutstoreViewModel();
        public OutstoreView()
        {
            InitializeComponent();

            DataContext = vm;
        }
    }
}
