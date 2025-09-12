using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReclamationsController : ControllerBase
    {
        private readonly IReclamationRepository _reclamationRepository;

        public ReclamationsController(IReclamationRepository reclamationRepository)
        {
            _reclamationRepository = reclamationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reclamation>>> GetAll()
        {
            var reclamations = await _reclamationRepository.GetAllAsync();
            return Ok(reclamations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reclamation>> GetById(int id)
        {
            var reclamation = await _reclamationRepository.GetByIdAsync(id);
            if (reclamation == null) return NotFound();
            return Ok(reclamation);
        }

        [HttpGet("statut/{statut}")]
        public async Task<ActionResult<IEnumerable<Reclamation>>> GetByStatut(int statut)
        {
            var reclamations = await _reclamationRepository.GetByStatutAsync(statut);
            return Ok(reclamations);
        }

        [HttpPost]
        public async Task<ActionResult<Reclamation>> Create(Reclamation reclamation)
        {
            await _reclamationRepository.AddAsync(reclamation);
            await _reclamationRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = reclamation.ReclamationId }, reclamation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Reclamation reclamation)
        {
            if (id != reclamation.ReclamationId) return BadRequest();

            _reclamationRepository.UpdateAsync(reclamation);
            await _reclamationRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reclamation = await _reclamationRepository.GetByIdAsync(id);
            if (reclamation == null) return NotFound();

            await _reclamationRepository.DeleteAsync(reclamation);
            await _reclamationRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}