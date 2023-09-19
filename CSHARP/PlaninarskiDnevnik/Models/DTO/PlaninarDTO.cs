using System.Reflection.Metadata.Ecma335;

namespace PlaninarskiDnevnik.Models.DTO
{
    public class PlaninarDTO
    {
        public int Sifra { get; set; }
        public string?  Ime { get; set; }
        public string? Prezime { get; set; }
        public string? Oib { get; set; }
        public string? PlDrustvo { get; set; }
        public string? Dnevnik { get; set; }
    }
}
