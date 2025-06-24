using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class LocationRepository : RepositoryBase, IRepository<Location>
    {
        public int Delete(Location entity)
        {
            //状态
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            //
            return db.SaveChanges();
        }

        public List<Location> FindAll(string keyword)
        {
            //列表查询
            return db.Location.Where(p => p.Name.Contains(keyword)).ToList();

        }

        public List<Location> GetAll()
        {
            return db.Location.ToList();
        }

        public int Insert(Location entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(Location entity)
        {
            //modified--修改
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
