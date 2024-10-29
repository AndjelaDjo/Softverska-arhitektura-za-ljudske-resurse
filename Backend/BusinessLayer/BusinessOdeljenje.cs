using DataLayer;
using DataLayer.Model;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BusinessOdeljenje : IBusinessOdeljenje
    {
        private readonly IOdeljenjeRepository _odeljenjeRepository;

        public BusinessOdeljenje(IOdeljenjeRepository odeljenjeRepository)
        {
            _odeljenjeRepository = odeljenjeRepository;
        }

        public List<Odeljenje> GetAllOdeljenja()
        {
            var odeljenja = _odeljenjeRepository.GetAllOdeljenja();
            if (odeljenja.Count > 0)
            {
                return odeljenja;
            }
            return new List<Odeljenje>();
        }

        public bool InsertOdeljenje(Odeljenje odeljenje)
        {
            if (string.IsNullOrEmpty(odeljenje.Naziv) || string.IsNullOrEmpty(odeljenje.Opis))
                return false;
            return _odeljenjeRepository.InsertOdeljenje(odeljenje);
        }

        public bool UpdateOdeljenje(Odeljenje odeljenje)
        {
            if (odeljenje.idOdeljenja == 0 || string.IsNullOrEmpty(odeljenje.Naziv) || string.IsNullOrEmpty(odeljenje.Opis))
                return false;
            return _odeljenjeRepository.UpdateOdeljenje(odeljenje);
        }

        public bool DeleteOdeljenje(Odeljenje odeljenje)
        {
            if (odeljenje.idOdeljenja == 0)
                return false;
            return _odeljenjeRepository.DeleteOdeljenje(odeljenje);
        }

        public decimal NumOfZaposlenihPoOdeljenju(string Naziv)
        {
            if (string.IsNullOrEmpty(Naziv))
                throw new ArgumentException("Naziv odeljenja neispravan.");

            return _odeljenjeRepository.NumOfZaposlenihPoOdeljenju(Naziv);
        }
    }
}
