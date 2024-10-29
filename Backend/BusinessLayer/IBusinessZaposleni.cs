using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBusinessZaposleni
    {
        List<Zaposleni> GetAllZaposleni();
        bool InsertZaposleni(Zaposleni zaposleni);
        bool UpdateZaposleni(Zaposleni zaposleni);
        bool DeleteZaposleni(Zaposleni zaposleni);
        int SumOfZaposleni();
        int NumOfMen();
        int NumOfWomen();
        decimal AvgYearsOfMen();
        decimal AvgYearsOfWomen();
        Zaposleni GetNameSurnameById(int idZaposlenog);
    }
}
