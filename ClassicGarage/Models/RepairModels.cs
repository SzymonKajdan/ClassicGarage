using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    public class RepairModels
    {
        public int ID { get; set; }
        
        public int CarId { get; set; }

        public String Name { get; set; }
        public String Description { get; set; }
        public double ServiceCost { get; set; }
        public int PartId { get; set; }
        public virtual ICollection<PartModels> Parts { get; set; }

    }
}