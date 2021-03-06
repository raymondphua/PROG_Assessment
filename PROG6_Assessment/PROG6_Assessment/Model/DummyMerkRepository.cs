﻿using DomainModel.Model;
using DomainModel.Repository;
using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Model
{
    public class DummyMerkRepository : IMerkRepository
    {
        public List<Merk> GetAll()
        {
            var merken = new List<Merk>();

            merken.Add(new Merk { MerkNaam = "Merk 1" });
            merken.Add(new Merk { MerkNaam = "Merk 2" });
            merken.Add(new Merk { MerkNaam = "Merk 3" });
            merken.Add(new Merk { MerkNaam = "Merk 4" });

            return merken;
        }

        public IEnumerable<Merk> GetAllMerken()
        {

            return GetAll();

        }

        public Merk Find(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Merk entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Merk entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Merk entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
