using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    public class CarModels
    {
        public int ID { get; set; }
        [Required]
        public String Brand { get; set; }
        [Required]
        public String Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public String Vin { get; set; }
        public String SerialNo { get; set; }
        public String Photo { get; set; }
        
        
        
        public DateTime PurchaseDate { get; set; }
   
        public DateTime SaleDate { get; set; }
        public double PurchasePrice { get; set; }
        
        public double SalePrice { get; set; }
        public double Budget { get; set; }
        public int OwnerID { get; set; }

        public  virtual OwnerModels Owner { get; set; }
        
        public virtual ICollection<RepairModels> RepairModels { get; set; }
        public virtual MarketModel Ad { get; set; }

    }
}