using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class CargoRepository : RepositoryBase, IRepository<Cargo>
    {
        public int Delete(Cargo entity)
        {
            //状态
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            //
            return db.SaveChanges();
        }

        public List<Cargo> FindAll(string keyword)
        {
            //列表查询
            return db.Cargo.Where(p=>p.Name.Contains(keyword)).ToList();

        }

        public List<Cargo> GetAll()
        {
            return db.Cargo.ToList();
        }

        public int Insert(Cargo entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(Cargo entity)
        {
            //modified--修改
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
