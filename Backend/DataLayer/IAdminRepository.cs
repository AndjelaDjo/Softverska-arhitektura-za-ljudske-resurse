using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IAdminRepository
    {
        List<Admin> GetAllAdmin();
        bool InsertAdmin(Admin admin);
        Admin GetByEmailAndPassword(string email, string password);
    }
}
