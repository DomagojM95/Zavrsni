using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojaAplikacija
{
    internal class Planinar:Entitet 
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Oib { get; set; }
        public string  PlaninarskoDrustvo { get; set; }
    }
}
