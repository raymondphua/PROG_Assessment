using DomainModel.Model;
using DomainModel.Repository;
using PROG6_Assessment.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace PROG6_Assessment.Model
{
    public class BoodschappenlijstjeRepository : IBoodschappenlijstjeRepository
    {
        private AppieContext dbContext;

        public BoodschappenlijstjeRepository()
        {
            dbContext = new AppieContext();
        }

        public List<Boodschappenlijstje> GetAll()
        {
            List<Boodschappenlijstje> lijst = new List<Boodschappenlijstje>();

            using (var context = new AppieContext())
            {
                lijst = context.Boodschappenlijstjes
                    .Include(x => x.Producten)
                    .ToList();
            }
            return lijst;
        }

        public Boodschappenlijstje Find(int id)
        {
            Boodschappenlijstje lijstje = null;
            using (var context = new AppieContext())
            {
                lijstje = context.Boodschappenlijstjes.Find(id);
            }

            return lijstje;
        }

        public void Create(Boodschappenlijstje entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    foreach (var item in entity.Producten)
                    {
                        context.Entry(entity).State = EntityState.Unchanged;
                    }

                    context.Boodschappenlijstjes.Add(entity);
                    context.SaveChanges();
                }
            }
        }

        public void Update(Boodschappenlijstje entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    var editEntity = context.Boodschappenlijstjes.SingleOrDefault(x => x.LijstId == entity.LijstId);

                    editEntity.LijstId = entity.LijstId;

                    context.SaveChanges();
                }
            }
        }

        public void Delete(Boodschappenlijstje entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    var removeEntity = context.Boodschappenlijstjes.SingleOrDefault(x => x.LijstId == entity.LijstId);

                    context.Boodschappenlijstjes.Remove(removeEntity);
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
