using Microsoft.AspNetCore.Mvc;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;

namespace PlaninarskiDnevnik.Controllers
{
    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class PlaninarController:ControllerBase
    {
        private readonly PlaninarskiDnevnikContext _context;

       public PlaninarController(PlaninarskiDnevnikContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPlaninar() 
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            try
            {
                var planinari = _context.Planinar.ToList();
                if (planinari == null || planinari.Count() == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.Planinar.ToList());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
           

        }

        [HttpPost]
        public IActionResult Post(Planinar planinar)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Planinar.Add(planinar);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, planinar);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }


        }



        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Planinar planinar)
        {
            if (sifra <= 0 || planinar == null)
            {
                return BadRequest();
            }

            try
            {
                var planinarBaza = _context.Planinar.Find(sifra);
                if (planinarBaza == null)
                {
                    return BadRequest();
                }

               planinarBaza.Ime= planinar.Ime;
                planinarBaza.Prezime = planinar.Prezime;
                planinarBaza.Oib= planinar.Oib;
                planinarBaza.Pldrustvo= planinar.Pldrustvo;

                _context.Planinar.Update(planinarBaza);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, planinarBaza);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);

            }


        }

        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            if (sifra == 0)
            {
                return BadRequest();
            }
            try
            {
                var planinarBaza = _context.Planinar.Find(sifra);
                if (planinarBaza == null)
                {
                    return BadRequest();
                }
                _context.Planinar.Remove(planinarBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");
            }
            catch (Exception ex)
            {

                return new JsonResult("{\"poruka\":\"Ne može se obrisati\"}");

            }


        }





    }
}
