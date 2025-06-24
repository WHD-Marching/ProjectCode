using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    //数据库上下文呢db
    public abstract class RepositoryBase
    {
        protected WarehouseDBEntities db = new WarehouseDBEntities();
    }
}
