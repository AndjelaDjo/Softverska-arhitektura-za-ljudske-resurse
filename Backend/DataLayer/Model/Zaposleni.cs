using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Zaposleni
    {
        public int idZaposlenog { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Pozicija { get; set; }
        public string Pol { get; set; }
        public int Starost { get; set; }
        public int idOdeljenja {  get; set; }
    }
}
