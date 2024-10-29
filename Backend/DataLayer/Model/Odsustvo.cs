using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Odsustvo
    {
        public int idOdsustva { get; set; }
        public DateTime datumPocetka { get; set; }
        public DateTime datumZavrsetka { get; set; }
        public string Razlog { get; set; }
        public int idZaposlenog { get; set; }
    }
}
