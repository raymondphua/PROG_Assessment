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
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductNaam { get; set; }
        public virtual Afdeling Afdeling { get; set; }
        public virtual ICollection<Merk> Merken { get; set; }
    }
}
