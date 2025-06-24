using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓储管理系统.Entities;

namespace 仓储管理系统.DAL
{
    public class MenuRepository : RepositoryBase, IMenuRepository
    {
        //所有菜单项
        public IEnumerable<Menu> GetAll()
        {
            return db.Menu.ToList();
        }

        //获取用户的菜单项,显示顺序排列
        public IEnumerable<Menu> GetUserMenus(int userId)
        {
            return db.UserMenu
                .Where(um => um.UserId == userId)
                .Select(um => um.Menu)
                .OrderBy(m => m.DisplayOrder)
                .ToList();
        }

        //层级查询
        public IEnumerable<Menu> GetAllWithHierarchy()
        {
            var menus = db.Menu.ToList();
            return menus.Where(m => m.ParentMenuId == null);
        }
    }
}
