﻿using DomainModel.Model;
using DomainModel.Repository;
using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Model
{
    public class DummyProductRepository : IProductRepository
    {
        public List<Product> GetAll()
        {
            var products = new List<Product>();

            products.Add(new Product { ProductNaam = "Wit Brood" });
            products.Add(new Product { ProductNaam = "Bruin Brood" });
            products.Add(new Product { ProductNaam = "Tijger Brood" });
            products.Add(new Product { ProductNaam = "Bedorven Brood" });

            return products;
        }

        public Product Find(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}