using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBusinessOdeljenje
    {
        List<Odeljenje> GetAllOdeljenja();
        bool InsertOdeljenje(Odeljenje odeljenje);
        bool UpdateOdeljenje(Odeljenje odeljenje);
        bool DeleteOdeljenje(Odeljenje odeljenje);
        decimal NumOfZaposlenihPoOdeljenju(string Naziv);
    }
}
