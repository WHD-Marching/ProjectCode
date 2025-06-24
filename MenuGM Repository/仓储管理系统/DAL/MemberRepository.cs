using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class MemberRepository : RepositoryBase, IRepository<Member>, IMemberRepository
    {

        public int Delete(Member entity)
        {
            //状态
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            //更新到基础数据库
            return db.SaveChanges();
        }

        public List<Member> FindAll(string keyword)
        {
            //查找字符串为空
            return db.Member.Where(p => p.Name.Contains(keyword)).ToList();
        }

        public List<Member> GetAll()
        {
            return db.Member.ToList();
        }

        public int Insert(Member entity)
        {
            //db.实体的对象.状态=.方法
            db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(Member entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();

        }


        public Member GetById(int id)
        {
            return db.Member.Find(id);
        }
    }
}
