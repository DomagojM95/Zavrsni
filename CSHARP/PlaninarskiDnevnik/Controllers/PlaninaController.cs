﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;
namespace PlaninarskiDnevnik.Controllers
{
    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class PlaninaController : ControllerBase
    {
        private readonly PlaninarskiDnevnikContext _context;

        public PlaninaController(PlaninarskiDnevnikContext context)
        {
            _context = context;
        }

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

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult PutPlanina(int sifra, Planina planina)
        {

            if (sifra <= 0 || planina == null)
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
                planinaBaza.Drzava = planina.Drzava;
                planinaBaza.Ime = planina.Ime;

                _context.Planina.Update(planinaBaza);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, planinaBaza);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }


        }



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