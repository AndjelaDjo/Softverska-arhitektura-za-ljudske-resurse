using DataLayer;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BusinessZaposleni : IBusinessZaposleni
    {
        private readonly IZaposleniRepository _zaposleniRepository;

        public BusinessZaposleni(IZaposleniRepository zaposleniRepository)
        {
            _zaposleniRepository = zaposleniRepository;
        }

        public List<Zaposleni> GetAllZaposleni()
        {
            var zaposleni = _zaposleniRepository.GetAllZaposleni();
            if (zaposleni.Count > 0)
            {
                return zaposleni;
            }
            return new List<Zaposleni>();
        }

        public bool InsertZaposleni(Zaposleni zaposleni)
        {
            if (string.IsNullOrEmpty(zaposleni.Ime) || string.IsNullOrEmpty(zaposleni.Prezime) || string.IsNullOrEmpty(zaposleni.Email) || string.IsNullOrEmpty(zaposleni.Pozicija) || string.IsNullOrEmpty(zaposleni.Pol) || zaposleni.Starost <= 0)
                return false;
            return _zaposleniRepository.InsertZaposleni(zaposleni);
        }

        public bool UpdateZaposleni(Zaposleni zaposleni)
        {
            if (zaposleni.idZaposlenog == 0 || string.IsNullOrEmpty(zaposleni.Ime) || string.IsNullOrEmpty(zaposleni.Prezime) || string.IsNullOrEmpty(zaposleni.Email) || string.IsNullOrEmpty(zaposleni.Pozicija) || string.IsNullOrEmpty(zaposleni.Pol) || zaposleni.Starost <= 0)
                return false;
            return _zaposleniRepository.UpdateZaposleni(zaposleni);
        }

        public bool DeleteZaposleni(Zaposleni zaposleni)
        {
            if (zaposleni.idZaposlenog == 0)
            {
                return false;
            }
            return _zaposleniRepository.DeleteZaposleni(zaposleni);
        }

        public int SumOfZaposleni()
        {
            return _zaposleniRepository.SumOfZaposleni();
        }

        public int NumOfMen()
        {
            return _zaposleniRepository.NumOfMen();
        }

        public int NumOfWomen()
        {
            return _zaposleniRepository.NumOfWomen();
        }

        public decimal AvgYearsOfMen()
        {
            return _zaposleniRepository.AvgYearsOfMen();
        }

        public decimal AvgYearsOfWomen()
        {
            return _zaposleniRepository.AvgYearsOfWomen();
        }

        public Zaposleni GetNameSurnameById(int idZaposlenog)
        {
            return _zaposleniRepository.GetNameSurnameById(idZaposlenog);
        }
    }
}
