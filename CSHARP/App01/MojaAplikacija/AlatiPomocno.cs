using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
 
    internal class AlatiPomocno
    {
      
        public static string UcitajString(string poruka, string greska)
        {
            string s = "";
            while (true)
            {
                Console.Write(poruka);
                s = Console.ReadLine();
                if (s != null && s.Trim().Length > 0)
                {
                    return s;
                }
                Console.WriteLine(greska);
            }
        }

            public static int UcitajCijeliBroj(string poruka, string greska)
        {
            int b;
            while (true)
            {
                Console.WriteLine(poruka);
                try
                {
                    b = int.Parse(Console.ReadLine());
                    if (b > 0)
                    {
                        return b;
                    }
                    Console.WriteLine(greska);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(greska);
                }
            }
        }
        public static int UcitajBrojRaspon(string poruka, string greska,int pocetak, int kraj)
        {
        while(true)
            {
                int b;
                Console.WriteLine(poruka);
                try
                {
                    b = int.Parse(Console.ReadLine());
                    if(b>= pocetak && b<= kraj)
                    {
                        return b;
                    }
                    Console.WriteLine(greska);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(greska);
                }
            }
        }
    }
}
