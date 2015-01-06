using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Repository
{
    public class DummyMerkRepository : IMerkRepository
    {
        public List<Merk> ToList()
        {
            var merken = new List<Merk>();

            merken.Add(new Merk { MerkNaam = "Merk 1" });
            merken.Add(new Merk { MerkNaam = "Merk 2" });
            merken.Add(new Merk { MerkNaam = "Merk 3" });
            merken.Add(new Merk { MerkNaam = "Merk 4" });

            return merken;
        }
    }
}
