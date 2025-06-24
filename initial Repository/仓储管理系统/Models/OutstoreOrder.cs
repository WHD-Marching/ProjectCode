using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;

namespace 仓储管理系统.Entities
{ 
    public partial class OutstoreOrder : ObservableObject
    {
        public string NameEx
        {

            get => Name;
            set
            {
                Name = value;
                OnPropertyChanged();
            } 
        }
    }
}

