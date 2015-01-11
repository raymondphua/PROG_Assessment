using DomainModel.Model;
using DomainModel.Repository;
using PROG6_Assessment.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Model
{
    public class ReceptRepository : IReceptRepository
    {

         private AppieContext dbContext;

        public ReceptRepository()
        {
            dbContext = new AppieContext();
        }

        public List<Recept> GetAll()
        {
            List<Recept> recepten = new List<Recept>();

            using (var context = new AppieContext())
            {
                recepten = context.Recepten
                    .Include(x => x.Producten)   
                    .ToList();
            }
            return recepten;
        }

        public Recept Find(int id)
        {
            Recept recept = null;
            using (var context = new AppieContext())
            {
                recept = context.Recepten.Find(id);
            }

            return recept;
        }

        public void Create(Recept entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    foreach (var item in entity.Producten)
                    {
                        context.Entry(item).State = EntityState.Unchanged;
                    }

                    context.Recepten.Add(entity);
                    context.SaveChanges();
                }
            }
        }

        public void Update(Recept entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    var editEntity = context.Recepten.SingleOrDefault(x => x.ReceptId == entity.ReceptId);

                    editEntity.ReceptId = entity.ReceptId;
                    editEntity.ReceptNaam = entity.ReceptNaam;

                    context.SaveChanges();
                }
            }
        }

        public void Delete(Recept entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    var removeEntity = context.Recepten.SingleOrDefault(x => x.ReceptId == entity.ReceptId);

                    context.Recepten.Remove(removeEntity);
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
