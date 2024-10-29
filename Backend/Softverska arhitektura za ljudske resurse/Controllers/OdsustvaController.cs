using BusinessLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Softverska_arhitektura_za_ljudske_resurse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdsustvaController : ControllerBase
    {
        private readonly IBusinessOdsustvo businessOdsustvo;

        public OdsustvaController(IBusinessOdsustvo businessOdsustvo)
        {
            this.businessOdsustvo = businessOdsustvo;
        }

        // GET: api/<OdsustvoController>
        [HttpGet("getAllOdsustva")]
        public List<Odsustvo> Get()
        {
            return businessOdsustvo.GetAllOdsustva();
        }

        // GET: api/<OdsustvaController>/byZaposleniId/{idZaposlenog}
        [HttpGet("getByZaposleniId/{idZaposlenog}")]
        public ActionResult<List<Odsustvo>> GetByZaposleniId(int idZaposlenog)
        {
            try
            {
                var odsustva = businessOdsustvo.OdsustvaPoZaposleniId(idZaposlenog);
                return Ok(odsustva);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<OdsustvaController>/byDatumPocetka/{datumPocetka}
        [HttpGet("getByDatumPocetka/{datumPocetka}")]
        public ActionResult<List<Odsustvo>> GetByDatumPocetka(DateTime datumPocetka)
        {
            try
            {
                var odsustva = businessOdsustvo.OdsustvaPoDatumu(datumPocetka);
                return Ok(odsustva);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/OdsustvaController/checkIfOdsustvoAllowed/{idZaposlenog}/{datumPocetka}/{datumZavrsetka}
        [HttpGet("checkIfOdsustvoAllowed/{idZaposlenog}/{datumPocetka}/{datumZavrsetka}")]
        public ActionResult<string> CheckIfOdsustvoAllowed(int idZaposlenog, string datumPocetka, string datumZavrsetka)
        {
            try
            {
                
                DateTime parsedDatumPocetka;
                DateTime parsedDatumZavrsetka;

                if (!DateTime.TryParseExact(datumPocetka, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDatumPocetka) ||
                    !DateTime.TryParseExact(datumZavrsetka, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDatumZavrsetka))
                {
                    return BadRequest("Invalid date format. Expected format is yyyy-MM-dd.");
                }

                if (parsedDatumZavrsetka <= parsedDatumPocetka)
                {
                    return BadRequest("End date must be after start date.");
                }

                var existingLeaves = businessOdsustvo.GetExistingLeaves(idZaposlenog);
                foreach (var leave in existingLeaves)
                {
                    if (parsedDatumPocetka < leave.datumZavrsetka && parsedDatumZavrsetka > leave.datumPocetka)
                    {
                        return BadRequest("Leave cannot be taken due to overlapping with existing leave.");
                    }
                }

                return Ok("Leave can be taken.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST api/<OdsustvoController>
        [HttpPost]
        public ActionResult Post([FromBody] Odsustvo odsustvo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = businessOdsustvo.InsertOdsustvo(odsustvo);
            if (!result)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }

            return Ok("Leave has been successfully entered.");
        }

        // PUT api/<OdsustvoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Odsustvo odsustvo)
        {
            if (id != odsustvo.idOdsustva)
            {
                return BadRequest("Leave ID does not match.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = businessOdsustvo.UpdateOdsustvo(odsustvo);
            if (!result)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }

            return Ok("Leave has been successfully updated.");
        }

        // DELETE api/<OdsustvoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var odsustvo = new Odsustvo { idOdsustva = id };

            var result = businessOdsustvo.DeleteOdsustvo(odsustvo);
            if (!result)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }

            return Ok("Leave has been successfully deleted.");
        }
    }
}
