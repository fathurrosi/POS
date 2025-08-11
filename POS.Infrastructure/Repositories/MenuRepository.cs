using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        readonly POSContext _context;

        public MenuRepository(POSContext context) { _context = context; }

        public List<Menu> GetAll()
        {
            List<Menu> list = new List<Menu>();
            try
            {
                list = this._context.Menus.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
    }
}
