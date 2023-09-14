using Microsoft.AspNetCore.Mvc;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;

namespace PlaninarskiDnevnik.Controllers
{

    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class IzletController:ControllerBase
    {
        private readonly PlaninarskiDnevnikContext _context;    


        public IzletController(PlaninarskiDnevnikContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetIzlet()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var izlet = _context.Izlet.ToList();
                if (izlet == null || izlet.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.Izlet.ToList());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }


        [HttpPost]
        public IActionResult PostIzlet(Izlet izlet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Izlet.Add(izlet);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, izlet);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }


        }

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult PutPlanina(int sifra, Izlet izlet)
        {

            if (sifra <= 0 || izlet == null)
            {
                return BadRequest();
            }
            try
            {
                var izletBaza = _context.Izlet.Find(sifra);
                if (izlet == null)
                {
                    return BadRequest();
                }
                izletBaza.Planina = izlet.Planina;
                izletBaza.Trajanje = izlet.Trajanje;
                izletBaza.Datum = izlet.Datum;
                izletBaza.Naziv = izlet.Naziv;
             

                _context.Izlet.Update(izletBaza);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, izletBaza);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }


        }



        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult BrisanjeIzlet(int sifra)
        {
            if (sifra == 0)
            {
                return BadRequest();
            }
            try
            {
                var izletBaza = _context.Izlet.Find(sifra);
                if (izletBaza == null)
                {
                    return BadRequest();
                }
                _context.Izlet.Remove(izletBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");
            }
            catch (Exception ex)
            {

                return new JsonResult("{\"poruka\":\"Ne moze se obrisati\"}");
            }

        }


    }
}
