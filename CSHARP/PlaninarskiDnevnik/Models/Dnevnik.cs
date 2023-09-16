using System.ComponentModel.DataAnnotations.Schema;

namespace PlaninarskiDnevnik.Models
{
    public class Dnevnik:Entitet
    {
        public string?  Naziv { get; set; }
        [ForeignKey("izlet")]
        public Izlet? Izlet { get; set; }
        [ForeignKey("planinar")]
        public Planinar? Planinar     { get; set; }
    }
}
