using DataLayer.Model;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IOdsustvoRepository
    {
        List<Odsustvo> GetAllOdsustva();
        bool InsertOdsustvo(Odsustvo odsustvo);
        bool UpdateOdsustvo(Odsustvo odsustvo);
        bool DeleteOdsustvo(Odsustvo odsustvo);
        List<Odsustvo> OdsustvaPoZaposleniId(int idZaposlenog);
        List<Odsustvo> OdsustvaPoDatumu(DateTime datumPocetka);
        List<Odsustvo> GetExistingLeaves(int idZaposlenog); 
    }
}
