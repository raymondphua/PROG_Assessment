using DomainModel.Model;
using DomainModel.Repository;
using PROG6_Assessment.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Model
{
    public class MerkRepository : IMerkRepository
    {
        private AppieContext dbContext;

        public MerkRepository()
        {
            dbContext = new AppieContext();
        }

        public List<Merk> GetAll()
        {
            List<Merk> merken = new List<Merk>();

            using (var context = new AppieContext())
            {
                merken = context.Merken.ToList();
            }

            return merken;
        }

        public Merk Find(int id)
        {
            Merk merk = null;
            using (var context = new AppieContext())
            {
                merk = context.Merken.Find(id);
            }

            return merk;
        }

        public void Create(Merk entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    foreach (var item in entity.Producten)
                    {
                        context.Entry(entity).State = System.Data.Entity.EntityState.Unchanged;
                    }
                    
                    context.Merken.Add(entity);
                    context.SaveChanges();
                }
            }
        }

        public void Update(Merk entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    //context.Entry(entity.Merk).State = System.Data.Entity.EntityState.Unchanged;
                    var editEntity = context.Merken.SingleOrDefault(x => x.MerkId == entity.MerkId);
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(Merk entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    var deleteEntity = context.Merken.SingleOrDefault(x => x.MerkId == entity.MerkId);
                    context.Merken.Remove(deleteEntity);
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
