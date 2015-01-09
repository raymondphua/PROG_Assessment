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
            List<Afdeling> afdelingen = new List<Afdeling>();

            using (var context = new AppieContext())
            {
                afdelingen = context.Afdelingen.ToList();
            }
            return afdelingen;
        }

        public Afdeling Find(int id)
        {
            Afdeling afdeling = null;
            using (var context = new AppieContext())
            {
                afdeling = context.Afdelingen.Find(id);
            }

            return afdeling;
        }

        public void Create(Afdeling entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    foreach (var item in entity.Producten)
                    {
                        context.Entry(entity).State = System.Data.Entity.EntityState.Unchanged;
                    }

                    context.Afdelingen.Add(entity);
                    context.SaveChanges();
                }
            }
        }

        public void Update(Afdeling entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    //context.Entry(entity.Merk).State = System.Data.Entity.EntityState.Unchanged;
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(Afdeling entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    context.Afdelingen.Remove(entity);
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
