using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    public class PartModels
    {
        public int ID { get; set; }
        public int CarID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String CatNo { get; set; }
        public double PurchasePrice { get; set; }
        public double PurchaseSale { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime SaleDate { get; set; }


    }
}