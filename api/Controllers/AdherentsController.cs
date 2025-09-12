using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdherentsController : ControllerBase
    {
        private readonly IAdherentRepository _adherentRepository;

        public AdherentsController(IAdherentRepository adherentRepository)
        {
            _adherentRepository = adherentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adherent>>> GetAll()
        {
            var adherents = await _adherentRepository.GetAllAsync();
            return Ok(adherents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Adherent>> GetById(int id)
        {
            var adherent = await _adherentRepository.GetByIdAsync(id);
            if (adherent == null) return NotFound();
            return Ok(adherent);
        }

        [HttpGet("matricule/{matricule}")]
        public async Task<ActionResult<Adherent>> GetByMatricule(string matricule)
        {
            var adherent = await _adherentRepository.GetByMatriculeAsync(matricule);
            if (adherent == null) return NotFound();
            return Ok(adherent);
        }

        [HttpPost]
        public async Task<ActionResult<Adherent>> Create(Adherent adherent)
        {
            await _adherentRepository.AddAsync(adherent);
            await _adherentRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = adherent.AdherentId }, adherent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Adherent adherent)
        {
            if (id != adherent.AdherentId) return BadRequest();

            _adherentRepository.UpdateAsync(adherent);
            await _adherentRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var adherent = await _adherentRepository.GetByIdAsync(id);
            if (adherent == null) return NotFound();

            await _adherentRepository.DeleteAsync(adherent);
            await _adherentRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}