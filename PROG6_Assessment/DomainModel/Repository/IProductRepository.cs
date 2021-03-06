﻿using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repository
{
    public interface IProductRepository : IRepository<Product>
    {

        IEnumerable<Product> GetAllProducts();

    }
}
