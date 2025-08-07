using Microsoft.Data.SqlClient;
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
    public class UserRepository : IUserRepository
    {
        readonly POSContext _context;

        public UserRepository(POSContext context) { _context = context; }

        public int Create(User item)
        {
            this._context.Entry(item).State = EntityState.Added;
            return this._context.SaveChanges();
        }

        public int Delete(string username)
        {
            User? item = this._context.Users.Find(username);
            if (item != null)
            {
                //this._context.Users.Where(t => t.username == username).ExecuteDelete();
                this._context.Users.Remove(item);
                return this._context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public int DeleteAll()
        {
            return this._context.Users.ExecuteDelete<User>();
        }

        public int DeleteByFK(int roleID)
        {
            string commentText = @"
delete t1
from [User] t1
inner join [UserRole] t2 on t1.Username = t2.Username
where t2.RoleID =@RoleID
";
            var roleIdParam = new Microsoft.Data.SqlClient.SqlParameter("@RoleID", roleID);
            int result = this._context.Database.ExecuteSqlRaw(commentText, roleIdParam);
            return result;
        }

        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            try
            {
                list = this._context.Users.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public User GetByKey(string username)
        {
            User? item = this._context.Users.Find(username);
            return item;
        }

        public int Update(User item)
        {
            this._context.Entry(item).State = EntityState.Modified;
            return this._context.SaveChanges();
        }
    }
}
