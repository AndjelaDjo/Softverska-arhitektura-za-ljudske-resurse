using BusinessLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Softverska_arhitektura_za_ljudske_resurse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdeljenjaController : ControllerBase
    {
        private readonly IBusinessOdeljenje businessOdeljenje;

        public OdeljenjaController(IBusinessOdeljenje businessOdeljenje)
        {
            this.businessOdeljenje = businessOdeljenje;
        }

        // GET: api/<OdeljenjaController>
        [HttpGet("getAllOdeljenja")]
        public List<Odeljenje> Get()
        {
            return businessOdeljenje.GetAllOdeljenja();
        }

        // GET: api/<OdeljenjaController>/numOfZaposlenihPoOdeljenju/{idOdeljenja}
        [HttpGet("getNumOfZaposlenihPoOdeljenju/{Naziv}")]
        public ActionResult<decimal> GetNumOfZaposlenihPoOdeljenju(string Naziv)
        {
            try
            {
                var numOfZaposlenih = businessOdeljenje.NumOfZaposlenihPoOdeljenju(Naziv);
                return Ok(numOfZaposlenih);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<OdeljenjaController>
        [HttpPost]
        public ActionResult Post([FromBody] Odeljenje odeljenje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = businessOdeljenje.InsertOdeljenje(odeljenje);
            if (!result)
            {
                return StatusCode(500, "Nastao je problem prilikom obrade zahteva.");
            }

            return Ok("Odeljenje je uspešno uneto.");
        }

        // PUT api/<OdeljenjaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Odeljenje odeljenje)
        {
            if (id != odeljenje.idOdeljenja)
            {
                return BadRequest("Id odeljenja se ne podudara.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = businessOdeljenje.UpdateOdeljenje(odeljenje);
            if (!result)
            {
                return StatusCode(500, "Nastao je problem prilikom obrade zahteva.");
            }

            return Ok("Odeljenje je uspešno ažurirano.");
        }

        // DELETE api/<OdeljenjaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var odeljenje = new Odeljenje { idOdeljenja = id };

            var result = businessOdeljenje.DeleteOdeljenje(odeljenje);
            if (!result)
            {
                return StatusCode(500, "Nastao je problem prilikom obrade zahteva.");
            }

            return Ok("Odeljenje je uspešno obrisano.");
        }

    }
}
