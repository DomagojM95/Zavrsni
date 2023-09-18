using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace PlaninarskiDnevnik.Models
{
    public class Planinar : Entitet
    {
        public string? Ime { get; set; }
        public string?  Prezime { get; set; }
        public string?  Pldrustvo { get; set; }
        public string? Oib { get; set; }
        [ForeignKey("dnevnik")]
        public Dnevnik Dnevnik { get; set; } 

    }
}
