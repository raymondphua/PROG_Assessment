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
using System.Data.Entity;

namespace PROG6_Assessment.Model
{
    public class ProductRepository : IProductRepository
    {
        private AppieContext dbContext;

        public ProductRepository()
        {
            dbContext = new AppieContext();
        }

        public IEnumerable<Product> GetAllProducts()
        {

            return GetAll();

        }

        public List<Product> GetAll()
        {
            using (var context = new AppieContext())
            {
                return context.Producten
                    .Include(x => x.Afdeling)
                    .Include(x => x.Merken)
                    .Include(x => x.Kortingen)
                    .ToList();
            }
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
                    context.Entry(entity.Afdeling).State = EntityState.Unchanged;
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
                    if (entity.Afdeling != null)
                    {
                        context.Entry(entity.Afdeling).State = EntityState.Unchanged;
                    }
                    var editEntity = context.Producten.SingleOrDefault(x => x.ProductId == entity.ProductId);

                    editEntity.ProductNaam = entity.ProductNaam;
                    editEntity.Afdeling = entity.Afdeling;
                    editEntity.Prijs = entity.Prijs;
                    //context.Entry(editEntity).CurrentValues.SetValues(entity);

                    context.SaveChanges();
                }
            }
        }

        public void Delete(Product entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    var removeEntity = context.Producten.SingleOrDefault(x => x.ProductId == entity.ProductId);
                    context.Producten.Remove(removeEntity);
                    context.SaveChanges();
                }
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
