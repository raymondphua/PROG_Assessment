using DomainModel.Model;
using DomainModel.Repository;
using PROG6_Assessment.DataBaseContext;
using PROG6_Assessment.Model;
using PROG6_Assessment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Model
{
    public class ProductRepository : IProductRepository
    {
        private AppieContext dbContext;

        public ProductRepository()
        {
            dbContext = new AppieContext();
        }

        public List<Product> GetAll()
        {
            return dbContext.Producten.ToList();
        }

        public Product Find(int id)
        {
            return dbContext.Producten.Find(id);
        }

        public void Create(Product entity)
        {
            if (entity != null)
            {
                dbContext.Producten.Add(entity);
            }
            Save();
        }

        public void Update(Product entity)
        {
            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
