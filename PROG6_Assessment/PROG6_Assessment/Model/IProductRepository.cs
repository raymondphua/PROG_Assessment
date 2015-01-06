using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROG6_Assessment.Model
{
    public interface IProductRepository
    {
        List<Product> ToList();
    }
}
