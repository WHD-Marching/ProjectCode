using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using 仓储管理系统.DAL;
using 仓储管理系统.Entities;

namespace 仓储管理系统.Entities
{
    /// <summary>
    ///   当前项
    /// </summary>
    public partial class Instore : ObservableObject
    {
        //物资
        public Cargo cargo = new Cargo();
        public Cargo Cargo
        {
            get => cargo;
            set { SetProperty(ref cargo, value); }
        }

        //库位
        public Location location = new Location();
        public Location Location
        {
            get => location;
            set { SetProperty(ref location, value); }
        }

        // InstoreOrder 扩展
        public string CargoName
        {
            get
            {
                var entity = new CargoRepository().GetAll().
                      FirstOrDefault(t => t.Id == this.CargoId);
                if (entity != null)
                {
                    return entity.Name;
                }
                return string.Empty;
            }
        }
        public string LocationName
        {
            get
            {
                var entity = new LocationRepository().GetAll().
                    FirstOrDefault(t => t.Id == this.LocationId);
                //Where(t => t.Id == this.LocationId).First();
                if (entity != null)
                {
                    return entity.Name;
                }
                return string.Empty;
            }
        }
    }
}
