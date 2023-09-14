using System.ComponentModel.DataAnnotations;

namespace PlaninarskiDnevnik.Models
{
    public abstract class Entitet
    {
        [Key]
        public int Sifra { get; set; }
    }
}
