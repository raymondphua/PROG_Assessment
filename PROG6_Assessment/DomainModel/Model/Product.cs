using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model
{
    public class Product
    {
        public Product()
        {
            Merken = new List<Merk>();
            Kortingen = new List<Korting>();
        }

        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductNaam { get; set; }
        [Required]
        public double Prijs { get; set; }
        public virtual Afdeling Afdeling { get; set; }
        public virtual ICollection<Merk> Merken { get; set; }
        public virtual ICollection<Korting> Kortingen { get; set; }
    }
}
