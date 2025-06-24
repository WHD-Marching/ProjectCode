using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class InstoreRepository:RepositoryBase,IRepository<Instore>
    {
        public int Delete(Instore entity)
        {
            //状态
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            //更新到基础数据库
            return db.SaveChanges();
        }

        public List<Instore> FindAll(string keyword)
        {
            throw new NotFiniteNumberException();
        }

        public List<Instore> GetAll()
        {
            return db.Instore.ToList();
        }

        public int Insert(Instore entity)
        {
            //db.实体的对象.状态=.方法
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(Instore entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();

        }
    }
}
