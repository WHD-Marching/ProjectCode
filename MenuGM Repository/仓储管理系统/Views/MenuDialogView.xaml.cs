using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using 仓储管理系统.Entities;
using 仓储管理系统.ViewModels;

namespace 仓储管理系统.Views
{
    /// <summary>
    /// MenuDialogView.xaml 的交互逻辑
    /// </summary>
    public partial class MenuDialogView : Window
    {
        public MenuDialogViewModel vm { get; private set; }

        public MenuDialogView(IEnumerable<Menu> allMenus, List<int> currentSelection)
        {
            InitializeComponent();
            vm  = new MenuDialogViewModel(allMenus, currentSelection);
            DataContext = vm;
        }

        // 分配事件
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MenuDialogViewModel vm)
            {
                vm.ConfirmCommand.Execute(null);
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}