using DataLayer.Model;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IBusinessOdsustvo
    {
        List<Odsustvo> GetAllOdsustva();
        bool InsertOdsustvo(Odsustvo odsustvo);
        bool UpdateOdsustvo(Odsustvo odsustvo);
        bool DeleteOdsustvo(Odsustvo odsustvo);
        List<Odsustvo> OdsustvaPoZaposleniId(int idZaposlenog);
        List<Odsustvo> OdsustvaPoDatumu(DateTime datumPocetka);
        string CheckIfOdsustvoAllowed(int idZaposlenog, DateTime datumPocetka, DateTime? datumZavrsetka = null);
        List<Odsustvo> GetExistingLeaves(int idZaposlenog);
    }
}
