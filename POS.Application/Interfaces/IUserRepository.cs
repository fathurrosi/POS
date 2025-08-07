using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public interface IUserRepository
    {
        User GetByKey(string username);
        List<User> GetAll();
        int Create(User item);
        int Update(User item);
        int Delete(string username);
        int DeleteByFK(int roleID);
        int DeleteAll();
    }
}
