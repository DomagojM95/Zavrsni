using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class ObradaIzlet
    {
        public List<Izlet> Izleti { get; }
        private Izbornik Izbornik;

        public ObradaIzlet(Izbornik Izbornik) : this()
        {
            this.Izbornik = Izbornik;
        }

        public ObradaIzlet()
        {
            Izleti = new List<Izlet>();

        }
        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s izletima");
            Console.WriteLine();
            Console.WriteLine("1. Pregled postojecih izleta");
            Console.WriteLine("2. Unos novih izleta");
            Console.WriteLine("3. Promjena postojecih izleta");
            Console.WriteLine("4. Brisanje izleta");
            Console.WriteLine("5. Povratak na glavni izbornik");

            switch (AlatiPomocno.UcitajBrojRaspon("Odaberite stavku izbornika izleta: ",
                 "Odabir mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    PregledIzleta();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosnIzleta();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaIzleta();
                    PrikaziIzbornik();
                    break;
                case 4:
                    if (Izleti.Count == 0)
                    {
                        Console.WriteLine("Nema vise izleta za brisanje");
                    }
                    else
                    { 
                        BrisanjeIzleta();
                    }
                   
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s izletima");
                    break;


            }
        }

        private void PromjenaIzleta()
        {
            PregledIzleta();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj izleta za obradu: ", "Nije dobro", 1, Izleti.Count());
            var i = Izleti[broj - 1];
            i.Sifra = AlatiPomocno.UcitajCijeliBroj("Unesite šifru izleta(" + i.Sifra + "):",
              "Unos mora biti pozitivni cijeli  broj");
            i.Naziv = AlatiPomocno.UcitajString("Unesite naziv izleta(" + i.Naziv + "):",
             "Unos obavezan");
            i.Trajanje = AlatiPomocno.UcitajCijeliBroj("Unesite trajanje(" + i.Trajanje + "):",
               "unos mora biti u satima (npr.8)");
            i.Datum = AlatiPomocno.UcitajCijeliBroj("Unesite datum izleta(" + i.Datum + "):",
              "unos neka bude dan,mjesec,godina sve zajedno(10042023)");
            i.Planina = UcitajPlaninu();
        }

        private void BrisanjeIzleta()
        {
            PregledIzleta();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj izleta za brisanje: ", "nije dobro", 1, Izleti.Count());
            Izleti.RemoveAt(broj - 1);
        }

        

        private void UnosnIzleta()
        {
            var i = new Izlet();
            i.Sifra = AlatiPomocno.UcitajCijeliBroj("Unesite sifru izleta", "Unos mora biti pozitivni cijeli broj");
            i.Naziv = AlatiPomocno.UcitajString("Unesi naziv izleta", "Unos obavezan");
            i.Datum = AlatiPomocno.UcitajCijeliBroj("Unesite datum izleta", "unos neka bude dan,mjesec,godina sve zajedno(10042023)");
            i.Trajanje = AlatiPomocno.UcitajCijeliBroj("Unesite trajanje izleta","unos mora biti u satima (npr. 8 )");
            i.Planina = UcitajPlaninu();


            Izleti.Add(i);
        }

        private Planina UcitajPlaninu()
        {
            Izbornik.ObradaPlanina.PrikaziPlanine();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj Planine za postavljanje na Izlet: "
                , "Nije dobro", 1, Izbornik.ObradaPlanina.Planine.Count());
            return Izbornik.ObradaPlanina.Planine[broj - 1];
        }

        public void PregledIzleta()
        {
            Console.WriteLine();
            Console.WriteLine("***** Dostupni izleti *****");
            Console.WriteLine("*****************************");
            int b = 1;
            foreach (Izlet izlet in Izleti)
            {
                Console.WriteLine("\t {0} {1} {2} {3}",b++,izlet.Datum,izlet.Trajanje,izlet.Naziv);
            }
            Console.WriteLine("******************************");
        }
    }
}
