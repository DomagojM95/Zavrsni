using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class Dnevnik:Entitet
    {
        public  string Naziv { get; set; }
        public Planinar Planinar { get; set; }
        public Izlet Izlet { get; set; }

    }
}
