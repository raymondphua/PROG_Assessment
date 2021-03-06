﻿using DomainModel.Model;
using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.DataBaseContext
{
    public class AppieContext : DbContext
    {
        public AppieContext() : base("name=DefaultConnection"){}
        public DbSet<Afdeling> Afdelingen { get; set; }
        public DbSet<Product> Producten { get; set; }
        public DbSet<Merk> Merken { get; set; }
        public DbSet<Korting> Kortingen { get; set; }
        public DbSet<Boodschappenlijstje> Boodschappenlijstjes { get; set; }
        public DbSet<Recept> Recepten { get; set; }
    }
}
