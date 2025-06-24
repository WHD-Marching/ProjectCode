using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class SupplierRepository : RepositoryBase, IRepository<Supplier>
    {
        public int Delete(Supplier entity)
        {
            //实体 状态
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            // 更新saveChanges()
            return db.SaveChanges();
        }

        public List<Supplier> FindAll(string keyword)
        {
            return db.Supplier.Where(p => p.Name.Contains(keyword)).ToList();
        }

        public List<Supplier> GetAll()
        {
            return db.Supplier.ToList();
        }

        public int Insert(Supplier entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(Supplier entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
