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
                case 3:
                    PromjenaPlaninara();
                    PrikaziIzbornik();
                    break;
                case 4:
                    if (Planinari.Count == 0)
                    {
                        Console.WriteLine("nema vise planinara za brisanje");
                    }
                    else
                    {
                        BrisanjePlaninara();
                    }
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gorov rad s planinarima");
                    break;


            }
        }

        private void BrisanjePlaninara()
        {
            PrikaziPlaninare();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj planinara za obradu: ", "Nije dobro", 1, Planinari.Count());
            Planinari.RemoveAt(broj - 1);
        }

        private void PromjenaPlaninara()
        {
            PrikaziPlaninare();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj planinara za obradu: ", "Nije dobro", 1, Planinari.Count());
            var p = Planinari[broj - 1];
            p.Sifra = AlatiPomocno.UcitajCijeliBroj("Unesite šifru Planinara("+p.Sifra+"):",
               "Unos mora biti pozitivni cijeli  broj");
            p.Ime = AlatiPomocno.UcitajString("Unesite ime Planinara("+p.Ime+"):", "Unos obavezan");
            p.Prezime = AlatiPomocno.UcitajString("Unesite prezime Planinara("+p.Prezime+"):", "Unos obavezan");
            p.Oib = AlatiPomocno.UcitajCijeliBroj("Unesite oib Planinara("+p.Oib+"):",
                "Unos mora  biti broj od 11 znamenki");
            p.PlaninarskoDrustvo = AlatiPomocno.UcitajString("Unesite planinarsko drustvo Planinara("+p.PlaninarskoDrustvo+")", "Unos obavezan");
        }

        private void UnosNovogPlaninara()
        {
            var p = new Planinar();
            p.Sifra = AlatiPomocno.UcitajCijeliBroj("Unesite šifru Planinara",
                "Unos mora biti pozitivni cijeli  broj");
            p.Ime = AlatiPomocno.UcitajString("Unesite ime Planinara", "Unos obavezan");
            p.Prezime = AlatiPomocno.UcitajString("Unesite prezime Planinara", "Unos obavezan");
            p.Oib = AlatiPomocno.UcitajCijeliBroj("Unesite oib Planinara",
                "Unos mora  biti broj od 11 znamenki");
            p.PlaninarskoDrustvo= AlatiPomocno.UcitajString("Unesite planinarsko drustvo Planinara", "Unos obavezan");
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
                Console.WriteLine("\t{0}. {1} {2} {3} {4}",b++,planinar.Ime,planinar.Prezime,planinar.Oib,planinar.PlaninarskoDrustvo);
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