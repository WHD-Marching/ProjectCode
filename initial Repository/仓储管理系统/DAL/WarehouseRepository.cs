using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class WarehouseRepository : RepositoryBase, IRepository<Warehouse>
    {
        public int Delete(Warehouse entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }

        public List<Warehouse> FindAll(string keyword)
        {
            // where 字符串匹配
            return db.Warehouse.Where(p => p.Name.Contains(keyword)).ToList();
        }

        public List<Warehouse> GetAll()
        {
            //return db.Warehouse.ToList();
            //// 按 Id 升序--OrderBy键值升序
            return db.Warehouse.OrderBy(w=>w.Id).ToList();
        }

        public int Insert(Warehouse entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(Warehouse entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
