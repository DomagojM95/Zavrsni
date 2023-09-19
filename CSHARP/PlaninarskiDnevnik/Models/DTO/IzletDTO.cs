namespace PlaninarskiDnevnik.Models.DTO
{
    public class IzletDTO
    {
        public int Sifra { get; set; }
        public string? Naziv { get; set; }

        public DateTime? Datum { get; set; }

        public DateTime? Trajanje { get; set; }

        public string? Planina { get; set; }

        public int SifraPlanina { get; set; }

    }
}
