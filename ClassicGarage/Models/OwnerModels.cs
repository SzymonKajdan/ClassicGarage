using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    public class OwnerModels
    {
        public int ID { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        
        public int PhoneNo { get; set; }
        [Required]
        
        public String Email{get;set;}

        public virtual ICollection<CarModels> Cars { get; set; }

    }
}