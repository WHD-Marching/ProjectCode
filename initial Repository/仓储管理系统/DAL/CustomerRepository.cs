using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class CustomerRepository : RepositoryBase, IRepository<Customer>
    {
        public int Delete(Customer entity)
        {
            //实体
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();
        }

        public List<Customer> FindAll(string keyword)
        {
            return db.Customer.Where(p => p.Name.Contains(keyword)).ToList();
        }

        public List<Customer> GetAll()
        {
            return db.Customer.ToList();
        }

        public int Insert(Customer entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(Customer entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
