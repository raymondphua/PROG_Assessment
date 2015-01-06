using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Repository
{
    public interface IMerkRepository
    {
        List<Merk> ToList();
    }
}
