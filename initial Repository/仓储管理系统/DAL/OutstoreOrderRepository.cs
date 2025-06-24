using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    internal class OutstoreOrderRepository : RepositoryBase, IRepository<OutstoreOrder>
    {
        public int Delete(OutstoreOrder entity)
        {
            //状态
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            //
            return db.SaveChanges();
        }

        public List<OutstoreOrder> FindAll(string keyword)
        {
            //列表查询
            return db.OutstoreOrder.Where(p => p.Name.Contains(keyword)).ToList();

        }

        public List<OutstoreOrder> GetAll()
        {
            return db.OutstoreOrder.ToList();
        }

        public int Insert(OutstoreOrder entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(OutstoreOrder entity)
        {
            //modified--修改
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
