using ClassicGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClassicGarage.DAL
{
    public class GarageContext: DbContext
    {
        public GarageContext() : base("name=cgDb")
        {

        }
      public  DbSet<MarketModel> Market { get; set; }
      public DbSet<RepairModels> Repairs { get; set; }
      public DbSet<PartModels> Parts { get; set; }
      public DbSet<OwnerModels> Owner { get; set; }
      public DbSet<CarModels> Cars { get; set; }
      
       

    }
}