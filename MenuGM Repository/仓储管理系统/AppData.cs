using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using 仓储管理系统.Entities;

namespace 仓储管理系统
{
    public class AppData 
    {
        // 当前用户--默认为空,在登录成功时'LoginVM'中member返回的MainWindow给到这里;
        public static Member CurrentUser { get; set; }

        public static MainWindow MainWindow { get; set; }
        // User控件
        public static ContentControl Container { get; set; }
        public static ContentControl MainRegion { get; set; }


    }
     
}
