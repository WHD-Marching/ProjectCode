using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓储管理系统.DAL
{
    public interface IRepository<T> where T:class
    {
        int Insert(T entity);
        int Update(T entity);
        int Delete(T entity);

        //查询
        List<T> GetAll();
        List<T> FindAll(string keyword);


    }
}
