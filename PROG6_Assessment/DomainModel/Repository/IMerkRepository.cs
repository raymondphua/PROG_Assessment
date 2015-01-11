using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repository
{
    public interface IMerkRepository : IRepository<Merk>
    {

        IEnumerable<Merk> GetAllMerken();


    }
}
