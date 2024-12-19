using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ringApi.Dto;
using ringApi.Enums;
using ringApi.Model;
using System.Net.NetworkInformation;

namespace ringApi.Controllers
{
    [ApiController]
    [Route("/api/ring")]
    public class RingController:ControllerBase
    {
        [HttpPost]
        public ActionResult<Ring> Post([FromServices] DataContext dataContext, [FromBody] RingRequest ringRequest) 
        {
            if (ModelState.IsValid) 
            {
                var ring = ringRequest.toModel();

                var allOwnerRings = dataContext.Rings.Where(r => r.OwnerRace == ring.OwnerRace).Count();

                int maxRings = ring.OwnerRace switch
                {
                    Races.ELFO => 3,
                    Races.ANAO => 7,
                    Races.HOMEM => 9,
                    Races.SAURON => 1,
                    _ => throw new ArgumentException("Raça inválida.")
                };

                // Verifica se o limite foi atingido
                if (allOwnerRings >= maxRings)
                {
                    return BadRequest($"A raça {ring.OwnerRace} só pode possuir no máximo {maxRings} anéis.");
                }

                dataContext.Rings.Add(ring);
                dataContext.SaveChanges();
                return Ok(ring);
            }

            return BadRequest("Preencha todos os campos corretamente!");
        }

        [HttpGet]
        public ActionResult<List<Ring>> Get([FromServices] DataContext dataContext)
            => dataContext.Rings.ToList();

        [HttpGet("{id:int}")]
        public ActionResult<List<Ring>> GetById([FromServices] DataContext dataContext, int id)
        {
            var ring = dataContext.Rings.FirstOrDefault(r => r.Id == id);
            if (ring == null)
                return NotFound(new { Message = "Id do anel não encontrado!" });

            return Ok(ring);
        }
        [HttpPut]
        public ActionResult<Ring> Put([FromServices] DataContext dataContext, [FromBody] Ring ring)
        {
            var findRing = dataContext.Rings.FirstOrDefault(r => r.Id == ring.Id);
            if (findRing == null)
                return NotFound(new { Message = "Id do anel não encontrado!" });


            int maxRings = ring.OwnerRace switch
            {
                Races.ELFO => 3,
                Races.ANAO => 7,
                Races.HOMEM => 9,
                Races.SAURON => 1,
                _ => throw new ArgumentException("Raça inválida.")
            };

            var allOwnerRings = dataContext.Rings.Where(r => r.OwnerRace == ring.OwnerRace).Count();

            // Verifica se o limite foi atingido
            if (allOwnerRings >= maxRings)
            {
                return BadRequest($"A raça {ring.OwnerRace} só pode possuir no máximo {maxRings} anéis.");
            }

            dataContext.Entry(findRing).State = EntityState.Detached;
            dataContext.Rings.Update(ring);
            dataContext.SaveChanges();

            return Ok(ring);
            
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromServices] DataContext dataContext, int id)
        {
            var ring = dataContext.Rings.Find(id);
            if (ring == null)
                return NotFound(new { Message = "Id do anel não encontrado!" });

            dataContext.Rings.Remove(ring);
            dataContext.SaveChanges();
            return NoContent();

        }
    }
}
