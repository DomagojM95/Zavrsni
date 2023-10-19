using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;
using PlaninarskiDnevnik.Models.DTO;

namespace PlaninarskiDnevnik.Controllers
{

    /// <summary>
    /// Namjenjeno za CRUD operacije nad Dnevnikom
    /// </summary>
    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class DnevnikController : ControllerBase
    {
        private readonly PlaninarskiDnevnikContext _context;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        public DnevnikController(PlaninarskiDnevnikContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Dohvaca sve dnevnike iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        /// 
        /// 
        /// Zavrsni/v1/Dnevnik
        /// 
        /// 
        /// </remarks>
        /// 
        /// <returns>Dnevnici u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        public IActionResult Get()
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var dnevnik = _context.Dnevnik
                    .Include(i => i.Izlet)
                    .Include(z => z.Planinar)
                    .ToList();

                if (dnevnik == null || dnevnik.Count == 0)
                {
                    return new EmptyResult();
                }

                List<DnevnikDTO> vrati = new();

                dnevnik.ForEach(d =>
                {
                    vrati.Add(new DnevnikDTO()
                    {
                        Sifra = d.Sifra,
                        Naziv = d.Naziv,
                        Izlet = d.Izlet.Naziv,
                        Planinar = d.Planinar?.Ime,
                        SifraPlaninar = d.Planinar.Sifra,
                        SifraIzlet = d.Izlet.Sifra
                    });
                });
                return Ok(vrati);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    ex);
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
                var s = _context.Dnevnik.Find(sifra);

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
        /// Dodaje dnevnike u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST Zavrsni/v1/Dnevnik
        ///    {Naziv:""}
        ///
        /// </remarks>
        /// <returns>Kreirani dnevnik u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 


        [HttpPost]
        public IActionResult Post(DnevnikDTO dnevnikDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dnevnikDTO.SifraPlaninar <= 0)
            {
                return BadRequest(ModelState);
            }
            if (dnevnikDTO.SifraIzlet <= 0)
            {
                return BadRequest(ModelState);
            }


            try
            {

                var planinar = _context.Planinar.Find(dnevnikDTO.SifraPlaninar);

                if (planinar == null)
                {
                    return BadRequest(ModelState);
                }

                var izlet = _context.Izlet.Find(dnevnikDTO.SifraIzlet);

                if (izlet == null)
                {
                    return BadRequest(ModelState);
                }

                Dnevnik d = new()
                {
                    Naziv = dnevnikDTO.Naziv,
                    Izlet = izlet,
                    Planinar = planinar,

                };

                _context.Dnevnik.Add(d);
                _context.SaveChanges();

                dnevnikDTO.Sifra = d.Sifra;
                dnevnikDTO.Izlet = izlet.Naziv;
                dnevnikDTO.Planinar = planinar.Ime;

                return Ok(dnevnikDTO);


            }
            catch (Exception ex)
            {
                return StatusCode(
                   StatusCodes.Status503ServiceUnavailable,
                   ex);
            }

        }


        /// <summary>
        /// Mijenja podatke postojećeg dnevnika u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT Zavrsni/v1//1
        ///
        /// {
        ///   "sifra": 0,
        ///   "ime": "string",
        ///   "prezime": "string",
        ///   "oib": "string",
        ///   "email": "string"
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra dnevnika koji se mijenja</param>  
        /// <returns>Svi poslani podaci od dnevnika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi dnevnika kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 


        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, DnevnikDTO dnevnikDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (sifra <= 0 || dnevnikDTO == null)
            {
                return BadRequest();
            }

            try
            {
                var planinar = _context.Planinar.Find(dnevnikDTO.SifraPlaninar);

                if (planinar == null)
                {
                    return BadRequest();
                }

                var izlet = _context.Izlet.Find(dnevnikDTO.SifraIzlet);

                if (izlet == null)
                {
                    return BadRequest();
                }

                var dnevnik = _context.Dnevnik.Find(sifra);

                if (dnevnik == null)
                {
                    return BadRequest();
                }

                dnevnik.Naziv = dnevnikDTO.Naziv;
                dnevnik.Planinar = planinar;
                dnevnik.Izlet = izlet;

                _context.Dnevnik.Update(dnevnik);
                _context.SaveChanges();

                dnevnikDTO.Sifra = sifra;
                dnevnikDTO.Planinar = planinar.Ime;
                dnevnikDTO.Izlet = izlet.Naziv;

                return Ok(dnevnikDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }


        }

        /// <summary>
        /// Briše dnevnik iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE Zavrsni/v1/Dnevnik/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra dnevnika koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi dnevnika kojeg želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response>

        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            if (sifra <= 0)
            {
                return BadRequest();
            }

            var dnevnikBaza = _context.Dnevnik.Find(sifra);
            if (dnevnikBaza == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Dnevnik.Remove(dnevnikBaza);
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
