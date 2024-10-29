using BusinessLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Softverska_arhitektura_za_ljudske_resurse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminiController : ControllerBase
    {
        private readonly IBusinessAdmin businessAdmin;
        public AdminiController(IBusinessAdmin businessAdmin)
        {
            this.businessAdmin = businessAdmin;
        }

        // GET: api/<AdminiController>
        [HttpGet("getAllAdmini")]
        public List<Admin> Get()
        {
            return businessAdmin.GetAllAdmin();
        }


        // POST api/<AdminiController>
        [HttpPost]
        public ActionResult Post([FromBody] Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = businessAdmin.InsertAdmin(admin);
            if (!result)
            {
                return StatusCode(500, "Nastao je problem prilikom obrade zahteva.");
            }

            return Ok("Admin je uspešno unet.");
        }

        // POST: api/Admini/Login
        [HttpPost("Login")]
        public ActionResult Login([FromBody] Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAdmin = businessAdmin.GetByEmailAndPassword(admin.Email, admin.Password);

            if (existingAdmin == null)
            {
                return NotFound("Pogrešan email ili lozinka.");
            }

            return Ok("Uspješno ste se prijavili.");
        }
    }
}
