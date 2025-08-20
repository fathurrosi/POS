using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Domain.Entities.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace POS.Infrastructure.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        readonly POSContext _context;

        public MenuRepository(POSContext context) { _context = context; }

        public List<Menu> GetAll()
        {
            return this._context.Menus.AsNoTracking().ToList();
        }

        public async Task<PagingResult<Menu>> GetDataPaging(int pageIndex, int pageSize)
        {
           
            var paramText = new SqlParameter("@text", "");
            var paramPageIndex = new SqlParameter("@pageIndex", pageIndex);
            var paramPageSize = new SqlParameter("@pageSize", pageSize);
            var paramTotalRecord = new SqlParameter("@totalRecord", SqlDbType.Int);
            paramTotalRecord.Direction = ParameterDirection.Output;

            PagingResult<Menu> result = new PagingResult<Menu>(pageIndex, pageSize);
            result.Items = await _context.Menus.FromSqlRaw("EXEC [dbo].[Usp_GetMenuPaging] @text, @pageIndex, @pageSize, @totalRecord OUTPUT",
                  paramText, paramPageIndex, paramPageSize, paramTotalRecord).ToListAsync();

            result.TotalCount = (int)paramTotalRecord.Value;

            return result;
        }


    }
}
