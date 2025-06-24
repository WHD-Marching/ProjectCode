using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.DAL;

namespace 仓储管理系统.Entities
{//要求: 与实体类的Location环境一致
    public partial class Location
    {

        //这是对实体类的扩展
        public string WarehouseName
        {
            get
            {
                /// <summary>
                /// @异常: 序列中不包含任何元素
                /// @代码:
                /// :找到其中一个仓库(First-返回序列的第一个元素)
                /// 
                /// var warehouse = new WarehouseRepository().GetAll().
                ///    Where(t => t.Id == this.WarehouseId).First();
                /// </summary>

                // 尝试 FirstOrDefault() 替代 First()
                // 如果不包含任何元素,则返回默认值;同步Id
                var warehouse = new WarehouseRepository().GetAll().
                    FirstOrDefault(t => t.Id == this.WarehouseId);
                // 更友好的默认值(对于已有的库位,但未分配仓库=>则"未分配仓库")
                return warehouse?.Name ?? "未分配仓库"; 


                // 判断仓库 空值--对应前端绑定WarehouseName
                //if (warehouse != null)
                //{
                //    return warehouse.Name;
                //}
                //return string.Empty;
            }
        }
    }
}
