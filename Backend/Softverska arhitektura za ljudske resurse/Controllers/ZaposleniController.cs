using BusinessLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Softverska_arhitektura_za_ljudske_resurse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZaposleniController : ControllerBase
    {
        private readonly IBusinessZaposleni businessZaposleni;

        public ZaposleniController(IBusinessZaposleni businessZaposleni)
        {
            this.businessZaposleni = businessZaposleni;
        }

        // GET: api/<ZaposleniController>
        [HttpGet("getAllZaposleni")]
        public List<Zaposleni> Get()
        {
            return businessZaposleni.GetAllZaposleni();
        }

        // GET: api/<ZaposleniController>/sumOfZaposleni
        [HttpGet("sumOfZaposleni")]
        public ActionResult<int> GetSumOfZaposleni()
        {
            return Ok(businessZaposleni.SumOfZaposleni());
        }

        // GET: api/<ZaposleniController>/numOfMen
        [HttpGet("numOfMen")]
        public ActionResult<int> NumOfMen()
        {
            return Ok(businessZaposleni.NumOfMen());
        }

        // GET: api/<ZaposleniController>/numOfWomen
        [HttpGet("numOfWomen")]
        public ActionResult<int> NumOfWomen()
        {
            return Ok(businessZaposleni.NumOfWomen());
        }


        // GET: api/<ZaposleniController>/avgYearsOfMen
        [HttpGet("avgYearsOfMen")]
        public ActionResult<decimal> AvgYearsOfMen()
        {
            return Ok(businessZaposleni.AvgYearsOfMen());
        }

        // GET: api/<ZaposleniController>/avgYearsOfWomen
        [HttpGet("avgYearsOfWomen")]
        public ActionResult<decimal> AvgYearsOfWomen()
        {
            return Ok(businessZaposleni.AvgYearsOfWomen());
        }

        // POST api/<ZaposleniController>
        [HttpPost]
        public ActionResult Post([FromBody] Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = businessZaposleni.InsertZaposleni(zaposleni);
            if (!result)
            {
                return StatusCode(500, "Nastao je problem prilikom obrade zahteva.");
            }

            return Ok("Zaposleni je uspešno unet.");
        }

        // PUT api/<ZaposleniController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Zaposleni zaposleni)
        {
            if (id != zaposleni.idZaposlenog)
            {
                return BadRequest("Id zaposlenog se ne podudara.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = businessZaposleni.UpdateZaposleni(zaposleni);
            if (!result)
            {
                return StatusCode(500, "Nastao je problem prilikom obrade zahteva.");
            }

            return Ok("Zaposleni je uspešno ažuriran.");
        }

        // DELETE api/<ZaposleniController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var zaposleni = new Zaposleni { idZaposlenog = id };

            var result = businessZaposleni.DeleteZaposleni(zaposleni);
            if (!result)
            {
                return StatusCode(500, "Nastao je problem prilikom obrade zahteva.");
            }

            return Ok("Zaposleni je uspešno obrisan.");
        }

        [HttpGet("getNameSurnameById/{id}")]
        public ActionResult<Zaposleni> GetNameSurnameById(int id)
        {
            var zaposleni = businessZaposleni.GetNameSurnameById(id);
            if (zaposleni == null)
            {
                return NotFound("Zaposleni nije pronađen.");
            }
            return Ok(zaposleni);
        }

    }
}
