using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    public class MarketModel
    {
        public int ID { get; set; }
        public int CarId { get; set; }
        public Boolean Active { get; set; }

       // public ICollection<CarModels> Cars{get;set;}

    }
}