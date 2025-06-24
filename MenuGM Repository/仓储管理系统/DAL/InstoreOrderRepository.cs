using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class InstoreOrderRepository:RepositoryBase,IRepository<InstoreOrder>
    {
        public int Delete(InstoreOrder entity)
        {
            //状态
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            //
            return db.SaveChanges();
        }

        public List<InstoreOrder> FindAll(string keyword)
        {
            //列表查询
            return db.InstoreOrder.Where(p => p.Name.Contains(keyword)).ToList();

        }

        public List<InstoreOrder> GetAll()
        {
            return db.InstoreOrder.ToList();
        }

        public int Insert(InstoreOrder entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(InstoreOrder entity)
        {
            //modified--修改
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
