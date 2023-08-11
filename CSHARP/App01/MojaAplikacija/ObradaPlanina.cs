using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class ObradaPlanina
    {
        public List<Planina> Planine { get; }
        public ObradaPlanina()
        {
            Planine = new List<Planina>();
        }
        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s planinama");
            Console.WriteLine();
            Console.WriteLine("1. Pregled postojecih planina");
            Console.WriteLine("2. Unos novih planina");
            Console.WriteLine("3. Promjena postojecih planina");
            Console.WriteLine("4. Brisanje planina");
            Console.WriteLine("5. Povratak na glavni izbornik");

            switch (AlatiPomocno.UcitajBrojRaspon("Odaberite stavku izbornika smjera: ",
                 "Odabir mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    PrikaziPlanine();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosNovihPlanina();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaPlanine();
                    PrikaziIzbornik();
                    break;
                case 4:
                    if (Planine.Count == 0)
                    {
                        Console.WriteLine("Nema vise planina za brisanje");
                    }
                    else
                    {
                        BrisanjePlanine();
                    }
                   
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s planinama");
                    break;
            }


        }

        private void BrisanjePlanine()
        {
            PrikaziPlanine();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj smjera za brisanje: ", "nije dobro", 1, Planine.Count());
            Planine.RemoveAt(broj - 1);
        }

        private void PromjenaPlanine()
        {
            PrikaziPlanine();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj planine za obradu: ", "Nije dobro", 1, Planine.Count());
            var p = Planine[broj - 1];
            p.Sifra = AlatiPomocno.UcitajCijeliBroj("Unesite šifru planine(" + p.Sifra + "):",
               "Unos mora biti pozitivni cijeli  broj");
            p.Ime = AlatiPomocno.UcitajString("Unesite ime planine(" + p.Ime + "):", "Unos obavezan");
            p.Drzava = AlatiPomocno.UcitajString("Unesite ime Drzave planine(" + p.Drzava + "):", "Unos obavezan");
            p.Visina = AlatiPomocno.UcitajCijeliBroj("Unesite visinu planine(" + p.Visina + "):",
               "Unos mora biti u metrima");
        }

        private void UnosNovihPlanina()
        {
            var p=new Planina();
            p.Sifra= AlatiPomocno.UcitajCijeliBroj("Unesite šifru Planine",
                "Unos mora biti pozitivni cijeli  broj");
            p.Ime = AlatiPomocno.UcitajString("Unesite ime  planine", "Unos obavezan");
            p.Drzava = AlatiPomocno.UcitajString("Unesite ime drzave planine", "Unos obavezan");
            p.Visina= AlatiPomocno.UcitajCijeliBroj("Unesite visinu planine",
                "Unos mora  biti u metrima");
            Planine.Add(p);
        }

        private void PrikaziPlanine()
        {
            Console.WriteLine();
            Console.WriteLine("***** Dostupne Planine *****");
            Console.WriteLine("*****************************");
            int b = 1;
            foreach (Planina planina in Planine )
            {
                Console.WriteLine("\t{0}. {1} {2} {3}", b++,planina.Ime,planina.Drzava,planina.Visina );
            }
            Console.WriteLine("******************************");
        }
    }
}
