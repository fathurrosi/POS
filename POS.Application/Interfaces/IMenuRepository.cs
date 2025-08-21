using Microsoft.Data.SqlClient;
using POS.Domain.Entities;
using POS.Domain.Entities.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public interface IMenuRepository
    {
        List<Menu> GetAll();
        List<Menu> GetByUsername(string username);
        public Task<PagingResult<Menu>> GetDataPaging(int pageIndex, int pageSize);

    }
}
