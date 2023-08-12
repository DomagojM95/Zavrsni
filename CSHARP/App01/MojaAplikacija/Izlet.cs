using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class Izlet:Entitet
    {
        public int Datum { get; set; }
        public int Trajanje { get; set; }
        public Planina Planina { get; set; }


    }
}
