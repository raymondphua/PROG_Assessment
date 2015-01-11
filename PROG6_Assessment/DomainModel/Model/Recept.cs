using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model
{
    public class Recept
    {
        public Recept()
        {
            this.Producten = new List<Product>();
        }

        [Key]
        public int ReceptId { get; set; }
        [Required]
        public string ReceptNaam { get; set; }
        public virtual ICollection<Product> Producten { get; set; }
    }
}
