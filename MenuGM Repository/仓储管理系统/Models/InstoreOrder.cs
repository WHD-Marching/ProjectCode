using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace 仓储管理系统.Entities
{
    public partial class InstoreOrder : ObservableObject
    {
        public string NameEx
        {
           
            get => Name;
            set
            {
                Name = value;
                OnPropertyChanged();
            }
            //属性或索引器不能用作out/ref值
            // set => SetProperty(ref Name,value);
        }
    }
}
