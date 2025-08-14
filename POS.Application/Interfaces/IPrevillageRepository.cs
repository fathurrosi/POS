using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public interface IPrevillageRepository
    {
        public List<Previllage> GetByUsername(string username);
    }
}
