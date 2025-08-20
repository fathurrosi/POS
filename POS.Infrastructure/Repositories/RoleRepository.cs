using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        readonly POSContext _context;

        public RoleRepository(POSContext context) { _context = context; }

        public int Create(Role item)
        {
            this._context.Entry(item).State = EntityState.Added;
            return this._context.SaveChanges();
        }

        public int Delete(int id)
        {
            Role? item = this._context.Roles.Find(id);
            if (item != null)
            {
                this._context.Roles.Remove(item);
                return this._context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public List<Role> GetAll()
        {
            return this._context.Roles.AsNoTracking().ToList();
        }
        public Role GetByKey(int id)
        {
            Role? item = this._context.Roles.Find(id);
            return item;
        }

        public List<Role> GetByUsername(string username)
        {
            var userParam = new SqlParameter("@Username", username);
            //Role? item = _context.Roles.FromSqlRaw($"EXECUTE dbo.Usp_GetRoleByUsername @Username").FirstOrDefault();
            List<Role> items = _context.Roles.FromSqlRaw("EXECUTE [dbo].[Usp_GetRoleByUsername] @Username", userParam).ToList();
            return items;
        }

        public int Update(Role item)
        {
            this._context.Entry(item).State = EntityState.Modified;
            return this._context.SaveChanges();
        }
    }
}
