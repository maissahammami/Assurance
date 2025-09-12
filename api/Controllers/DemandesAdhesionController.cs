using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemandesAdhesionController : ControllerBase
    {
        private readonly IDemandeAdhesionRepository _demandeAdhesionRepository;

        public DemandesAdhesionController(IDemandeAdhesionRepository demandeAdhesionRepository)
        {
            _demandeAdhesionRepository = demandeAdhesionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemandeAdhesion>>> GetAll()
        {
            var demandes = await _demandeAdhesionRepository.GetAllAsync();
            return Ok(demandes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DemandeAdhesion>> GetById(int id)
        {
            var demande = await _demandeAdhesionRepository.GetByIdAsync(id);
            if (demande == null) return NotFound();
            return Ok(demande);
        }

        [HttpGet("statut/{statut}")]
        public async Task<ActionResult<IEnumerable<DemandeAdhesion>>> GetByStatut(int statut)
        {
            var demandes = await _demandeAdhesionRepository.GetByStatutAsync(statut);
            return Ok(demandes);
        }

        [HttpPost]
        public async Task<ActionResult<DemandeAdhesion>> Create(DemandeAdhesion demandeAdhesion)
        {
            await _demandeAdhesionRepository.AddAsync(demandeAdhesion);
            await _demandeAdhesionRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = demandeAdhesion.DemandeAdhesionId }, demandeAdhesion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DemandeAdhesion demandeAdhesion)
        {
            if (id != demandeAdhesion.DemandeAdhesionId) return BadRequest();

            _demandeAdhesionRepository.UpdateAsync(demandeAdhesion);
            await _demandeAdhesionRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var demande = await _demandeAdhesionRepository.GetByIdAsync(id);
            if (demande == null) return NotFound();

            await _demandeAdhesionRepository.DeleteAsync(demande);
            await _demandeAdhesionRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}