using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Repository
{
    public class DummyProductRepository : IProductRepository
    {
        public List<Product> ToList()
        {
            var products = new List<Product>();

            products.Add(new Product { ProductNaam = "Wit Brood" });
            products.Add(new Product { ProductNaam = "Bruin Brood" });
            products.Add(new Product { ProductNaam = "Tijger Brood" });
            products.Add(new Product { ProductNaam = "Bedorven Brood" });

            return products;
        }
    }
}
