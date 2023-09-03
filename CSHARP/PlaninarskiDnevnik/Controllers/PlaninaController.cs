using Microsoft.AspNetCore.Mvc;
using PlaninarskiDnevnik.Models;
namespace PlaninarskiDnevnik.Controllers
{
    [ApiController]
    [Route("Zavrsni/v1/[controller]")]
    public class PlaninaController:ControllerBase
    {

      
        [HttpGet]
        public IActionResult UcitajListuPlanina()
        {
            var lista= new List<Planina>()
            {
                new (){Ime="Biokovo"},
                new (){Ime="Velebit"},
                new (){Ime="Papuk"},
            };
            return  new JsonResult(lista);
        }
        [HttpPost]
        public IActionResult Izmjene(Planina lista) 
        {

            return new JsonResult(lista);
        
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
