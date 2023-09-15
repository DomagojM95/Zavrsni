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
        public IActionResult Post(IzletDTO izletDTO)
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
                Izlet g = new()
                {
                     Sifra=izletDTO.Sifra,
                   Naziv=izletDTO.Naziv,
                   Datum=izletDTO.Datum,
                   Trajanje=izletDTO.Trajanje,
                   
                   Planina=planina
                   
                };

                _context.Izlet.Add(g);
                _context.SaveChanges();

                izletDTO.Sifra = g.Sifra;

               
                return Ok(izletDTO);
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
