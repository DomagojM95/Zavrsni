using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class ObradaDnevnik
    {
        private Izbornik Izbornik;
        public List<Dnevnik> Dnevnici { get; set; }

        public ObradaDnevnik(Izbornik Izbornik) : this()
        {
            this.Izbornik = Izbornik;
        }

        public ObradaDnevnik() 
        {
           Dnevnici=new List<Dnevnik>();
           
        }
        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s dnevnikom");
            Console.WriteLine();
            Console.WriteLine("1. Pregled postojecih dnevnika");
            Console.WriteLine("2. Unos novih dnevnika");
            Console.WriteLine("3. Promjena postojecih dnevnika");
            Console.WriteLine("4. Brisanje dnevnika");
            Console.WriteLine("5. Povratak na glavni izbornik");

            switch(AlatiPomocno.UcitajBrojRaspon("Odaberite stavku izbornika dnevnika: ",
                 "Odabir mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    PregledDnevnika();
                    PrikaziIzbornik();
                        break;
                case 2:
                    UnosDnevnika();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaDnevnika();
                    PrikaziIzbornik();
                    break;
                case 4:
                    if (Dnevnici.Count == 0)
                    {
                        Console.WriteLine("Nema vise dnevnika za brisanje");
                    }
                    else
                    {
                        BrisanjeDnevnika();
                    }
                    BrisanjeDnevnika();
                    PrikaziIzbornik();
                    break;
            }


        }

        private void BrisanjeDnevnika()
        {

            PregledDnevnika();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj planinara za obradu: ", "Nije dobro", 1, Dnevnici.Count());
            Dnevnici.RemoveAt(broj - 1);
        }

        private void PromjenaDnevnika()
        {
            PrikaziIzbornik();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj dnevnika za obradu: ", "Nije dobro", 1, Dnevnici.Count());
            var i = Dnevnici[broj - 1];
            i.Sifra = AlatiPomocno.UcitajCijeliBroj("Unesite šifru dnevnika(" + i.Sifra + "):",
              "Unos mora biti pozitivni cijeli  broj");
            i.Naziv = AlatiPomocno.UcitajString("Unesite naziv dnevnika(" + i.Naziv + "):",
              "Unos obavezan");
            i.Planinar = UcitajPlaninara();
            i.Izlet = UcitajIzlet();
        }

        private void UnosDnevnika()
        {
            var d = new Dnevnik();
            d.Sifra = AlatiPomocno.UcitajCijeliBroj("Unesite sifru dnevnika", "Unos mora biti pozitivni cijeli broj");
            d.Naziv = AlatiPomocno.UcitajString("Upišite naziv dnevnika", "Unos obavezan");
            d.Planinar = UcitajPlaninara();
            d.Izlet = UcitajIzlet();

            Dnevnici.Add(d);

        }

        private Izlet UcitajIzlet()
        {
            Izbornik.ObradaIzlet.PregledIzleta();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj Izleta za postavljanje u dnevnik: "
                , "Nije dobro", 1, Izbornik.ObradaIzlet.Izleti.Count());
            return Izbornik.ObradaIzlet.Izleti[broj - 1];
        }

        private Planinar UcitajPlaninara()
        {
            Izbornik.ObradaPlaninar.PrikaziPlaninare();
            int broj = AlatiPomocno.UcitajBrojRaspon("Odaberi redni broj Planinara za postavljanje u dnevnik: "
                , "Nije dobro", 1, Izbornik.ObradaPlaninar.Planinari.Count());
            return Izbornik.ObradaPlaninar.Planinari[broj - 1];
        }

        private void PregledDnevnika()
        {
            Console.WriteLine();
            Console.WriteLine("***** Dostupni dnevnici *****");
            Console.WriteLine("*****************************");
            int b = 1;
            foreach (Dnevnik dnevnik in Dnevnici)
            {
                Console.WriteLine("\t {0} {1} {2}", dnevnik.Naziv,dnevnik.Planinar.Ime,dnevnik.Izlet.Naziv);
            }
            Console.WriteLine("******************************");

        }
       
    }
}
