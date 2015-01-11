using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model
{
    public class Merk
    {
        [Key]
        public int MerkId { get; set; }
        [Required]
        public string MerkNaam { get; set; }
        public virtual Product Product { get; set; }
        public double Multiplier { get; set; }
    }
}
