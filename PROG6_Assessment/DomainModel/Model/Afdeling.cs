using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model
{
    public class Afdeling
    {
        public Afdeling()
        {
            this.Producten = new List<Product>();
        }

        [Key]
        public int AfdelingId { get; set; }
        [Required]
        public string AfdelingNaam { get; set; }
        public virtual ICollection<Product> Producten { get; set; }
    }
}
