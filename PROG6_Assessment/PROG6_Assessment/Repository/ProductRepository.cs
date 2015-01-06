using PROG6_Assessment.DataBaseContext;
using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Repository
{
    public class ProductRepository : IProductRepository
    {
        private AppieContext dbContext;

        public ProductRepository()
        {
            dbContext = new AppieContext();
        }

        public List<Product> ToList()
        {
            return dbContext.Producten.ToList();
        }
    }
}
