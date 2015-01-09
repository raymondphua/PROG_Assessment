using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.ViewModel
{
    public class AllListViewModel
    {
        public AfdelingListViewModel AfdelingList { get; set; }
        public MerkListViewModel MerkList { get; set; }
        public ProductListViewModel ProductList { get; set; }

        public AllListViewModel()
        {
            AfdelingList = new AfdelingListViewModel();
            MerkList = new MerkListViewModel();
            ProductList = new ProductListViewModel();
        }
    }
}
