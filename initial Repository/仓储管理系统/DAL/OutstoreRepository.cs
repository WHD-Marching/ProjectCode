using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    internal class OutstoreRepository : RepositoryBase, IRepository<Outstore>
    {
        public int Delete(Outstore entity)
        {
            //状态
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            //
            return db.SaveChanges();
        }

        public List<Outstore> FindAll(string keyword)
        { 
            //异常
            throw new NotImplementedException();
        }

        public List<Outstore> GetAll()
        {
            return db.Outstore.ToList();
        }

        public int Insert(Outstore entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(Outstore entity)
        {
            //modified--修改
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
