using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public interface IUserMenuRepository
    {
        void Add(UserMenu userMenu);
        void Remove(int userId, int menuId);
        IEnumerable<UserMenu> GetByUserId(int userId);
    }
}
