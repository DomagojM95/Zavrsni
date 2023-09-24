using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;
namespace PlaninarskiDnevnik.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad Planinom
    /// </summary>


    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class PlaninaController:ControllerBase
    {
        private readonly PlaninarskiDnevnikContext _context;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>


        public PlaninaController(PlaninarskiDnevnikContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaca sve Planine iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        /// 
        /// 
        /// Zavrsni/v1/Planina
        /// 
        /// 
        /// </remarks>
        /// 
        /// <returns>planina u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        public IActionResult GetPlanina()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var planine = _context.Planina.ToList();
                if (planine == null || planine.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.Planina.ToList());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }
        /// <summary>
        /// Dodaje planine u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST Zavrsni/v1/Planina
        ///    {Naziv:""}
        ///
        /// </remarks>
        /// <returns>Kreirani planina u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]
        public IActionResult PostPlanina(Planina planina) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Planina.Add(planina);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, planina);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }


        }

        /// <summary>
        /// Mijenja podatke postojeće Planine u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT Zavrsni/v1/planina/1
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
        /// <param name="sifra">Šifra planine koja se mijenja</param>  
        /// <returns>Svi poslani podaci od planine</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi planine kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 


        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult PutPlanina(int sifra, Planina planina)
        {

            if (sifra <= 0|| planina==null)
            {
                return BadRequest();
            }
            try
            {
                var planinaBaza = _context.Planina.Find(sifra);
                if (planina == null)
                {
                    return BadRequest();
                }
                planinaBaza.Visina = planina.Visina;
                planinaBaza.Drzava=planina.Drzava;
                planinaBaza.Ime=planina.Ime;

                _context.Planina.Update(planinaBaza);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, planinaBaza);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

           
        }

        /// <summary>
        /// Briše izlete iz planine
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE Zavrsni/v1/planina/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra planine koja se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi planine koje želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response>



        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult Brisanje(int sifra)
        {
            if (sifra == 0)
            {
                return BadRequest();
            }
            try
            {
                var planinaBaza = _context.Planina.Find(sifra);
                if (planinaBaza == null)
                {
                    return BadRequest();
                }
                _context.Planina.Remove(planinaBaza);
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
