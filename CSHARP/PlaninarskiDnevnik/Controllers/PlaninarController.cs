using Microsoft.AspNetCore.Mvc;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;

namespace PlaninarskiDnevnik.Controllers
{

    /// <summary>
    /// Namjenjeno za CRUD operacije nad Planinarom
    /// </summary>

    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class PlaninarController:ControllerBase
    {
        private readonly PlaninarskiDnevnikContext _context;



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// 

        public PlaninarController(PlaninarskiDnevnikContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaca svih Planinara iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        /// 
        /// 
        /// Zavrsni/v1/Planinar
        /// 
        /// 
        /// </remarks>
        /// 
        /// <returns>planinar u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

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

        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {

            if (sifra <= 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var s = _context.Planinar.Find(sifra);

                if (s == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, s);
                }

                return new JsonResult(s);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }




        /// <summary>
        /// Dodaje planinara u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST Zavrsni/v1/Planinar
        ///    {Naziv:""}
        ///
        /// </remarks>
        /// <returns>Kreiran planinar u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 



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
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                   ex.Message);
            }



        }

        /// <summary>
        /// Mijenja podatke postojećeg planinara u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT Zavrsni/v1/planinar/1
        ///
        /// {
        ///   "sifra": 0,
        ///   "naziv": "string",
        ///   "prezime": "string",
        ///   "oib": "string",
        ///   "email": "string"
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra planinara koja se mijenja</param>  
        /// <returns>Svi poslani podaci od planine</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi planinara kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 



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

        /// <summary>
        /// Briše planinara iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE Zavrsni/v1/planinar/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra planinara koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi planinara kojeg želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response>

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
