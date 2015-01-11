using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model
{
    public class Boodschappenlijstje
    {

        public Boodschappenlijstje()
        {
            this.Producten = new List<Product>();
        }

        [Key]
        public int LijstId { get; set; }
        [Required]
        public virtual ICollection<Product> Producten { get; set; }

    }
}
