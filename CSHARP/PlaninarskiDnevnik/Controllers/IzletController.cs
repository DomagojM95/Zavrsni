using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;
using PlaninarskiDnevnik.Models.DTO;

namespace PlaninarskiDnevnik.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad Izletom
    /// </summary>

    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class IzletController : ControllerBase
    {
        private readonly PlaninarskiDnevnikContext _context;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>


        public IzletController(PlaninarskiDnevnikContext context)
        {
            _context = context;

        }

        /// <summary>
        /// Dohvaca sve Izlete iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        /// 
        /// 
        /// Zavrsni/v1/Izlet
        /// 
        /// 
        /// </remarks>
        /// 
        /// <returns>Izleti u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        public IActionResult GetIzlet()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var izlet = _context.Izlet.Include(i => i.Planina).ToList();
                if (izlet == null || izlet.Count == 0)
                {
                    return new EmptyResult();
                }

                List<IzletDTO> vrati = new();

                izlet.ForEach(i =>
                {
                    vrati.Add(new IzletDTO
                    {
                        Sifra = i.Sifra,
                        Naziv = i.Naziv,
                        Datum = i.Datum,
                        Trajanje = i.Trajanje,
                        Planina = i.Planina.Ime


                    });
                });

                return Ok(vrati);
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
                var s = _context.Izlet.Find(sifra);

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
        /// Dodaje izlete u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST Zavrsni/v1/Izlet
        ///    {Naziv:""}
        ///
        /// </remarks>
        /// <returns>Kreirani izlet u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]
        public IActionResult PostIzlet(IzletDTO izletDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (izletDTO.SifraPlanina <= 0)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var planina = _context.Planina.Find(izletDTO.SifraPlanina);

                if (planina == null)
                {
                    return BadRequest(ModelState);
                }

                Izlet i = new()
                {
                    Naziv = izletDTO.Naziv,
                    Datum = izletDTO.Datum,
                    Trajanje = izletDTO.Trajanje,
                    Planina = planina


                };

                _context.Izlet.Add(i);
                _context.SaveChanges();

                izletDTO.Sifra = i.Sifra;
                izletDTO.Planina = planina.Ime;

                return Ok(izletDTO);


            }
            catch (Exception ex)
            {
                return StatusCode(
                   StatusCodes.Status503ServiceUnavailable,
                   ex);
            }

        }

        /// <summary>
        /// Mijenja podatke postojećeg izleta u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT Zavrsni/v1/izlet/1
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
        /// <param name="sifra">Šifra izleta koji se mijenja</param>  
        /// <returns>Svi poslani podaci od dnevnika</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi dnevnika kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 


        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, IzletDTO izletDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (sifra <= 0 || izletDTO == null)
            {
                return BadRequest();
            }

            try
            {
                var planina = _context.Planina.Find(izletDTO.SifraPlanina);

                if (planina == null)
                {
                    return BadRequest();
                }

                var izlet = _context.Izlet.Find(sifra);

                if (izlet == null)
                {
                    return BadRequest();
                }

                izlet.Naziv = izletDTO.Naziv;
                izlet.Planina = planina;
                izlet.Datum = izletDTO.Datum;
                izlet.Trajanje = izletDTO.Trajanje;

                _context.Izlet.Update(izlet);
                _context.SaveChanges();

                izletDTO.Sifra = sifra;
                izletDTO.Planina = planina.Ime;


                return Ok(izletDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }


        }

        /// <summary>
        /// Briše izlete iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE Zavrsni/v1/izlet/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra izleta koji se briše</param>  
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

            var izletBaza = _context.Izlet.Find(sifra);
            if (izletBaza == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Izlet.Remove(izletBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");

            }
            catch (Exception ex)
            {

                return new JsonResult("{\"poruka\":\"Ne može se obrisati\"}");

            }
        }



        [HttpGet]
        [Route("{sifra:int}/planine")]
        public IActionResult GetPlanine(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var izlet = _context.Izlet
                    .Include(i => i.Planine)
                    .FirstOrDefault(i => i.Sifra == sifra);

                if (izlet == null)
                {
                    return BadRequest();
                }

                if (izlet.Planine == null || izlet.Planine.Count == 0)
                {
                    return new EmptyResult();
                }

                List<PlaninaDTO> vrati = new();
                izlet.Planine.ForEach(p =>
                {
                    vrati.Add(new PlaninaDTO()
                    {
                        Sifra = p.Sifra,
                        Ime = p.Ime,
                        Visina=p.Visina,
                        Drzava=p.Drzava
                    });
                });
                return Ok(vrati);
            }
            catch (Exception ex)
            {
                return StatusCode(
                        StatusCodes.Status503ServiceUnavailable,
                        ex.Message);
            }


        }

        [HttpPost]
        [Route("{sifra:int}/dodaj/{planinaSifra:int}")]
        public IActionResult DodajPlaninu(int sifra, int planinaSifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0 || planinaSifra <= 0)
            {
                return BadRequest();
            }

            try
            {

                var izlet = _context.Izlet
                    .Include(i => i.Planine)
                    .FirstOrDefault(i => i.Sifra == sifra);

                if (izlet == null)
                {
                    return BadRequest();
                }

                var planina = _context.Planina.Find(planinaSifra);

                if (planina == null)
                {
                    return BadRequest();
                }

                // napraviti kontrolu da li je taj polaznik već u toj grupi
                izlet.Planine.Add(planina);

                _context.Izlet.Update(izlet);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(
                       StatusCodes.Status503ServiceUnavailable,
                       ex.Message);

            }

        }

        [HttpDelete]
        [Route("{sifra:int}/dodaj/{planinaSifra:int}")]
        public IActionResult ObrisiPlaninu(int sifra, int planinaSifra)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0 || planinaSifra <= 0)
            {
                return BadRequest();
            }

            try
            {

                var izlet = _context.Izlet
                    .Include(i => i.Planine)
                    .FirstOrDefault(i => i.Sifra == sifra);

                if (izlet == null)
                {
                    return BadRequest();
                }

                var planina = _context.Planina.Find(planinaSifra);

                if (planina == null)
                {
                    return BadRequest();
                }


                izlet.Planine.Remove(planina);

                _context.Izlet.Update(izlet);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(
                       StatusCodes.Status503ServiceUnavailable,
                       ex.Message);

            }

        }



    }
}

        


    
   
