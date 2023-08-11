using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class Izlet:Entitet
    {
        public DateTime Datum { get; set; }
        public TimeOnly Trajanje { get; set; }

    }
}
