using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 仓储管理系统.Controls
{
    /// <summary>
    /// Card.xaml 的交互逻辑
    /// </summary>
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }


        //绑定内容
        public string CardTag
        {
            get { return (string)GetValue(CardTagProperty); }
            set { SetValue(CardTagProperty, value); }
        }
        // 对应委托方法
        public static readonly DependencyProperty CardTagProperty =
            DependencyProperty.Register("CardTag", typeof(string), typeof(Card), new PropertyMetadata(
                "Title",new PropertyChangedCallback(
                    OnTagPropertyChangedCallback)));
        private static void OnTagPropertyChangedCallback(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            if (d is Card card)
            {
                card._tag.Text = e.NewValue as string;
            }
        }

        // 数据
        public object CardContent
        {
            get { return (object)GetValue(CardContentProperty); }
            set { SetValue(CardContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Constent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CardContentProperty =
            DependencyProperty.Register("CardContent", typeof(object), typeof(Card), new PropertyMetadata(
                0,new PropertyChangedCallback(OnContentPropertyChangedCallback)));
        private static void OnContentPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Card card)
            {
                // 试图转换为字符串,但整数会失败
                card._content.Text = e.NewValue?.ToString() ?? string.Empty;
            }
        }
    }
}
