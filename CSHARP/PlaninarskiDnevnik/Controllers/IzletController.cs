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
                var izlet = _context.Izlet.Include(i=>i.Planina).ToList();
                if (izlet == null || izlet.Count == 0)
                {
                    return new EmptyResult();
                }

                List<IzletDTO> vrati = new();

                izlet.ForEach(i =>
                {
                vrati.Add(new IzletDTO{
                    Sifra=i.Sifra,
                    Naziv=i.Naziv,
                    Datum=i.Datum,
                    Trajanje=i.Trajanje,
                    Planina=i.Planina.Ime
                    

                });
                });

                return Ok(vrati);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }

        }

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
                    Planina=planina
                 
                    
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
                
              izlet.Naziv=izletDTO.Naziv;
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
        [Route("{sifra:int}/izleti")]
        public IActionResult GetIzleti(int sifra)
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
                    .Include(p =>p.Planina)
                    .FirstOrDefault(i => i.Sifra == sifra);

                if (izlet == null)
                {
                    return BadRequest();
                }

                if (izlet.Planina == null || izlet.Planina.Count==0)
                {
                    return new EmptyResult();
                }

                List<PolaznikDTO> vrati = new();
                grupa.Polaznici.ForEach(p =>
                {
                    vrati.Add(new PolaznikDTO()
                    {
                        Sifra = p.Sifra,
                        Ime = p.Ime,
                        Prezime = p.Prezime,
                        Oib = p.Oib,
                        Email = p.Email
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
}
