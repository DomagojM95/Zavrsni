using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PlaninarskiDnevnik.Models
{
    public class Izlet : Entitet
    {

        public string? Naziv { get; set; }

        public DateTime? Datum { get; set; }

        public DateTime? Trajanje { get; set; }


        [ForeignKey("planina")]
        public Planina Planina { get; set; }

        


    }
}
