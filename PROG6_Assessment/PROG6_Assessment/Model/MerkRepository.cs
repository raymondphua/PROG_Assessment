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
            return dbContext.Merken.ToList();
        }

        public Merk Find(int id)
        {
            return dbContext.Merken.Find(id);
        }

        public void Create(Merk entity)
        {
            if (entity != null)
            {
                dbContext.Merken.Add(entity);
            }
            Save();
        }

        public void Update(Merk entity)
        {
            if (entity != null)
            {
                dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
            Save();
        }

        public void Delete(Merk entity)
        {
            if (entity != null)
            {
                dbContext.Merken.Remove(entity);
            }
            Save();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
