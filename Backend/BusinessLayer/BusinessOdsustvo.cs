using DataLayer;
using DataLayer.Model;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BusinessOdsustvo : IBusinessOdsustvo
    {
        private readonly IOdsustvoRepository _odsustvoRepository;

        public BusinessOdsustvo(IOdsustvoRepository odsustvoRepository)
        {
            _odsustvoRepository = odsustvoRepository;
        }

        public List<Odsustvo> GetAllOdsustva()
        {
            var odsustva = _odsustvoRepository.GetAllOdsustva();
            return odsustva.Count > 0 ? odsustva : new List<Odsustvo>();
        }

        public bool InsertOdsustvo(Odsustvo odsustvo)
        {
            if (odsustvo.datumPocetka >= odsustvo.datumZavrsetka ||
                odsustvo.datumPocetka == DateTime.MinValue ||
                odsustvo.datumZavrsetka == DateTime.MinValue ||
                string.IsNullOrEmpty(odsustvo.Razlog))
            {
                return false;
            }
            return _odsustvoRepository.InsertOdsustvo(odsustvo);
        }

        public bool UpdateOdsustvo(Odsustvo odsustvo)
        {
            if (odsustvo.idOdsustva == 0 ||
                odsustvo.datumPocetka == DateTime.MinValue ||
                odsustvo.datumZavrsetka == DateTime.MinValue ||
                odsustvo.datumPocetka >= odsustvo.datumZavrsetka ||
                string.IsNullOrEmpty(odsustvo.Razlog))
            {
                return false;
            }
            return _odsustvoRepository.UpdateOdsustvo(odsustvo);
        }

        public bool DeleteOdsustvo(Odsustvo odsustvo)
        {
            if (odsustvo.idOdsustva == 0)
            {
                return false;
            }
            return _odsustvoRepository.DeleteOdsustvo(odsustvo);
        }

        public List<Odsustvo> OdsustvaPoZaposleniId(int idZaposlenog)
        {
            if (idZaposlenog <= 0)
            {
                throw new ArgumentException("ID zaposlenog neispravan.");
            }

            var odsustva = _odsustvoRepository.OdsustvaPoZaposleniId(idZaposlenog);
            return odsustva ?? new List<Odsustvo>();
        }

        public List<Odsustvo> OdsustvaPoDatumu(DateTime datumPocetka)
        {
            if (datumPocetka == DateTime.MinValue)
            {
                throw new ArgumentException("Datum početka mora biti validan datum.");
            }

            var odsustva = _odsustvoRepository.OdsustvaPoDatumu(datumPocetka);
            return odsustva ?? new List<Odsustvo>();
        }

        public string CheckIfOdsustvoAllowed(int idZaposlenog, DateTime datumPocetka, DateTime? datumZavrsetka = null)
        {
            if (idZaposlenog <= 0)
            {
                throw new ArgumentException("Employee ID is invalid.");
            }

            if (datumZavrsetka.HasValue && datumZavrsetka.Value <= datumPocetka)
            {
                throw new ArgumentException("End date must be after start date.");
            }

            var existingLeaves = _odsustvoRepository.GetExistingLeaves(idZaposlenog);

            foreach (var leave in existingLeaves)
            {
                if (datumZavrsetka.HasValue)
                {
                    if (datumPocetka < leave.datumZavrsetka && datumZavrsetka.Value > leave.datumPocetka)
                    {
                        throw new ArgumentException("Leave cannot be taken due to overlapping with existing leave.");
                    }
                }
                else
                {
                    if (datumPocetka < leave.datumZavrsetka && datumPocetka > leave.datumPocetka)
                    {
                        throw new ArgumentException("Leave cannot be taken due to overlapping with existing leave.");
                    }
                }
            }

            return "Leave can be taken.";
        }

        public List<Odsustvo> GetExistingLeaves(int idZaposlenog)
        {
            if (idZaposlenog <= 0)
            {
                throw new ArgumentException("Employee ID is invalid.");
            }

            var existingLeaves = _odsustvoRepository.GetExistingLeaves(idZaposlenog);
            return existingLeaves ?? new List<Odsustvo>();
        }
    }
}
