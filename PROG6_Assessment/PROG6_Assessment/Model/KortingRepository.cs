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
    public class KortingRepository : IKortingRepository
    {

        public List<DomainModel.Model.Korting> GetAll()
        {
            List<Korting> list = new List<Korting>();
            using (var context = new AppieContext())
            {
                list = context.Kortingen
                    .Include(x => x.Product)
                    .ToList();
            }
            return list;
        }

        public DomainModel.Model.Korting Find(int id)
        {
            Korting korting = null;
            using (var context = new AppieContext())
            {
                korting = context.Kortingen.Find(id);
            }
            return korting;
        }

        public void Create(DomainModel.Model.Korting entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    if (entity.Product != null)
                    {
                        context.Entry(entity.Product).State = EntityState.Unchanged;
                    }

                    context.Kortingen.Add(entity);
                    context.SaveChanges();
                }
            }
        }

        public void Update(DomainModel.Model.Korting entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    if (entity.Product != null)
                    {
                        context.Entry(entity.Product).State = EntityState.Unchanged;
                    }
                    var editEntity = context.Kortingen.SingleOrDefault(x => x.KortingId == entity.KortingId);

                    editEntity.KortingId = entity.KortingId;
                    editEntity.Coupon = entity.Coupon;
                    editEntity.StartDatum = entity.StartDatum;
                    editEntity.EindDatum = entity.EindDatum;
                    editEntity.Product = entity.Product;

                    context.SaveChanges();
                }
            }
        }

        public void Delete(DomainModel.Model.Korting entity)
        {
            using (var context = new AppieContext())
            {
                if (entity != null)
                {
                    var removeEntity = context.Kortingen.SingleOrDefault(x => x.KortingId == entity.KortingId);

                    context.Kortingen.Remove(removeEntity);
                    context.SaveChanges();
                }
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
