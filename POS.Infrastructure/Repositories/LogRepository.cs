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
    public class LogRepository : ILogRepository
    {
        readonly POSContext _context;

        public LogRepository(POSContext context) { _context = context; }
        public int Create(Log item)
        {
            this._context.Entry(item).State = EntityState.Added;
            return this._context.SaveChanges();
        }
    }
}
