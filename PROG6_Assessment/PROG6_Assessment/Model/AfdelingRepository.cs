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
    public class AfdelingRepository : IAfdelingRepository
    {
        private AppieContext dbContext;

        public List<Afdeling> GetAll()
        {
            return dbContext.Afdelingen.ToList();
        }

        public Afdeling Find(int id)
        {
            return dbContext.Afdelingen.Find(id);
        }

        public void Create(Afdeling entity)
        {
            if (entity != null)
            {
                dbContext.Afdelingen.Add(entity);
            }
            Save();
        }

        public void Update(Afdeling entity)
        {
            if (entity != null)
            {
                dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
            Save();
        }

        public void Delete(Afdeling entity)
        {
            if (entity != null)
            {
                dbContext.Afdelingen.Remove(entity);
            }
            Save();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
