using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;
using PlaninarskiDnevnik.Models.DTO;

namespace PlaninarskiDnevnik.Controllers
{
    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class DnevnikController:ControllerBase
    {
        private readonly PlaninarskiDnevnikContext _context;

        public DnevnikController(PlaninarskiDnevnikContext context)
        {
            _context = context;
        }

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
                    Planinar=planinar,
                  
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
                dnevnik.Izlet= izlet;

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
