using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class ObradaDnevnik
    {
        public ObradaDnevnik() 
        {

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


        }
    }
}
