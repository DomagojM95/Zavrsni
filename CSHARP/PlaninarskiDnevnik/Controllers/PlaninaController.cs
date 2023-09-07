using Microsoft.AspNetCore.Mvc;
using PlaninarskiDnevnik.Data;
using PlaninarskiDnevnik.Models;
namespace PlaninarskiDnevnik.Controllers
{
    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class PlaninaController:ControllerBase
    {
        private readonly PlaninaContext _context;

        public PlaninaController(PlaninaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult listaPlanina()
        {

            return new JsonResult(_context.Planina.ToList());
        }


        [HttpPost]
        public IActionResult Izmjene(Planina planina) 
        {

            _context.Planina.Add(planina);
            _context.SaveChanges();


            return new JsonResult(planina);
        
        }

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult PromjenaImena(int sifra,Planina planina)
        {
            return Created("Nova planina: ",planina);
        }



        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult Brisanje(int sifra)
        {
            return StatusCode(StatusCodes.Status200OK, "{\"obrisano\":true}");
        }

    }
}
