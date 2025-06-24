using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class UserMenuRepository : IUserMenuRepository
    {
        private readonly WarehouseDBEntities _context;

        public UserMenuRepository(WarehouseDBEntities context)
        {
            _context = context;
        }

        public void Add(UserMenu userMenu)
        {
            _context.UserMenu.Add(userMenu);
            _context.SaveChanges();
        }

        public void Remove(int userId, int menuId)
        {
            var userMenu = _context.UserMenu.FirstOrDefault(um => um.UserId == userId && um.MenuId == menuId);
            if (userMenu != null)
            {
                _context.UserMenu.Remove(userMenu);
                _context.SaveChanges();
            }
        }

        public IEnumerable<UserMenu> GetByUserId(int userId)
        {
            return _context.UserMenu.Where(um => um.UserId == userId).ToList();
        }
    }
}