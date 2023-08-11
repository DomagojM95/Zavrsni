using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class Planina:Entitet
    {
        public string Ime{ get; set; }
        public string Drzava { get; set; }
        public int Visina { get; set; }
    }
}
