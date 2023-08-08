using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class ObradaPlaninar
    {
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

            }
        }
    }
}
/////////////////// obrada switch planinari