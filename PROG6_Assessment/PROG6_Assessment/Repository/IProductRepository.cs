using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROG6_Assessment.Repository
{
    public interface IProductRepository
    {
        List<Product> ToList();
    }
}
