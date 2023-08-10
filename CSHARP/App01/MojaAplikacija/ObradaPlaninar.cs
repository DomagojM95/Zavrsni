using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class ObradaPlaninar
    {
        public ObradaPlaninar()
        {
            Planinari = new List<Planinar>();
            TestniPodaci();
        }

        public List<Planinar>Planinari { get;  }
        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s Planinarima");
            Console.WriteLine();
            Console.WriteLine("1. Pregled postojecih Planinara");
            Console.WriteLine("2. Unos novog Planinara");
            Console.WriteLine("3. Promjena postojeceg Planinara");
            Console.WriteLine("4. Brisanje Planinara");
            Console.WriteLine("5. Povratak na glavni izbornik");
            
            switch (AlatiPomocno.UcitajBrojRaspon("Odaberite stavku izbornika smjera: ",
                 "Odabir mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    PrikaziPlaninare();
                    PrikaziIzbornik();
                        break;
                case 2:
                    UnosNovogPlaninara();
                    PrikaziIzbornik();
                    break;

            }
        }

        private void UnosNovogPlaninara()
        {
            var p = new Planinar();
            p.Sifra = AlatiPomocno.UcitajCijeliBroj("Unesite šifru Planinara",
                "Unos mora biti pozitivni cijeli  broj");
            p.Ime = AlatiPomocno.UcitajString("Unesite ime Planinara", "Unos obavezan");
            p.Prezime = AlatiPomocno.UcitajString("Unesite prezime Planinara", "Unos obavezan");
            Planinari.Add(p);
        }

        private void PrikaziPlaninare()
        {
            Console.WriteLine();
            Console.WriteLine("***** Dostupni Planinari*****");
            Console.WriteLine("*****************************");
            int b = 1;
            foreach(Planinar planinar in Planinari)
            {
                Console.WriteLine("\t{0}. {1} {2}",b++,planinar.Ime,planinar.Prezime);
            }
            Console.WriteLine("******************************");
        }
        private void TestniPodaci()
        {
            Planinari.Add(new Planinar() { Ime = "Marko" });
            Planinari.Add(new Planinar() { Ime = "ivan" });
            Planinari.Add(new Planinar() { Ime = "Maja" });
        }
    }
}
/////////////////// obrada switch planinari