using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class Izbornik
    {
        public ObradaPlaninar ObradaPlaninar { get; set; }
        public ObradaPlanina ObradaPlanina { get;  }
        public ObradaIzlet ObradaIzlet { get; set; }

        public ObradaDnevnik ObradaDnevnik { get; set; }

        

        public Izbornik() 
        {
            ObradaPlaninar =new ObradaPlaninar();
           ObradaPlanina=new ObradaPlanina();
            ObradaIzlet=new ObradaIzlet(this);
            ObradaDnevnik = new ObradaDnevnik(this);
            PozdravnaPoruka();
            PrikaziIzbornik();
        }
        private void PozdravnaPoruka()
        {
            Console.WriteLine("###############################");
            Console.WriteLine("##### Planinarski Dnevnik #####");
            Console.WriteLine("###############################");
        }
        private void PrikaziIzbornik()
        {
            Console.WriteLine("Glavni izbornik");
            Console.WriteLine("1. Dnevnik");
            Console.WriteLine("2. Planinar");
            Console.WriteLine("3. Izlet");
            Console.WriteLine("4. Planina");
            Console.WriteLine("5.Izlaz iz programa");

            switch(AlatiPomocno.UcitajBrojRaspon("Odaberite stavku izbornika: ","Odabir mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    ObradaDnevnik.PrikaziIzbornik();
                    Console.WriteLine("Rad s dnevnicima");
                    PrikaziIzbornik();
                    break;
                case 2:
                    ObradaPlaninar.PrikaziIzbornik();
                    Console.WriteLine("Rad s planinarima");
                    PrikaziIzbornik();
                    break;
                case 3:
                    ObradaIzlet.PrikaziIzbornik();
                    Console.WriteLine("Rad s izletima");
                    PrikaziIzbornik();
                    break;
                case 4:
                    ObradaPlanina.PrikaziIzbornik();
                    Console.WriteLine("Rad s planinama");
                    PrikaziIzbornik();
                    break;
            }
        }
    }
}
