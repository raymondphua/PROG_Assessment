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
            List<Product> products = null;

            using (var context = new AppieContext())
            {
                products = context.Producten.ToList();
        }
            return products;
        }

        public Product Find(int id)
        {
            Product product = null;
            using (var context = new AppieContext())
            {
                product = context.Producten.Find(id);
        }

            return product;
        }

        public void Create(Product entity)
        {
            using (var context = new AppieContext())
            {
            if (entity != null)
            {
                    context.Entry(entity.Merk).State = System.Data.Entity.EntityState.Unchanged;
                    context.Producten.Add(entity);
                    context.SaveChanges();
                }
            }
        }

        public void Update(Product entity)
        {
            using (var context = new AppieContext())
            {
            if (entity != null)
            {
                var editEntity = dbContext.Producten.SingleOrDefault(x => x.ProductId == entity.ProductId);
                dbContext.Entry(editEntity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(Product entity)
        {
            using (var context = new AppieContext())
            {
            if (entity != null)
            {
                var removeEntity = dbContext.Producten.SingleOrDefault(x => x.ProductId == entity.ProductId);
                dbContext.Producten.Remove(removeEntity);
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
