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
        public Izbornik() 
        {
            ObradaPlaninar =new ObradaPlaninar();
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
            Console.WriteLine("1. Planinar");
            Console.WriteLine("2. Izlet");
            Console.WriteLine("3. Planina");
            Console.WriteLine("4.Izlaz iz programa");

            switch(AlatiPomocno.UcitajBrojRaspon("Odaberite stavku izbornika: ","Odabir mora biti od 1 do 4", 1, 4))
            {
                case 1:
                    ObradaPlaninar.PrikaziIzbornik();
                    Console.WriteLine("Rad s planinarima");
                    break;
            }
        }
    }
}
