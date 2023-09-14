namespace PlaninarskiDnevnik.Models
{
    public class Dnevnik:Entitet
    {
        public string?  Naziv { get; set; }
        public Izlet Izlet { get; set; }
        public Planinar Planinar     { get; set; }
    }
}
