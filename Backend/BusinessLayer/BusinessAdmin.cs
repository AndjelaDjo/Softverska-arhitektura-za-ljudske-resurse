using DataLayer;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BusinessAdmin : IBusinessAdmin
    {
        private readonly IAdminRepository _adminRepository;

        public BusinessAdmin(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public List<Admin> GetAllAdmin()
        {
            var admin = _adminRepository.GetAllAdmin();
            if (admin.Count > 0)
            {
                return admin;
            }
            return new List<Admin>();
        }

        public Admin GetByEmailAndPassword(string email, string password)
        {
            return _adminRepository.GetByEmailAndPassword(email, password);
        }

        public bool InsertAdmin(Admin admin)
        {
            if (string.IsNullOrEmpty(admin.Email) || string.IsNullOrEmpty(admin.Password))
                return false;
            return _adminRepository.InsertAdmin(admin);
        }
    }
}
