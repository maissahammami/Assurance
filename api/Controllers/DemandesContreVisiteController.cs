using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemandesContreVisiteController : ControllerBase
    {
        private readonly IDemandeContreVisiteRepository _demandeContreVisiteRepository;

        public DemandesContreVisiteController(IDemandeContreVisiteRepository demandeContreVisiteRepository)
        {
            _demandeContreVisiteRepository = demandeContreVisiteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemandeContreVisite>>> GetAll()
        {
            var demandes = await _demandeContreVisiteRepository.GetAllAsync();
            return Ok(demandes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DemandeContreVisite>> GetById(int id)
        {
            var demande = await _demandeContreVisiteRepository.GetByIdAsync(id);
            if (demande == null) return NotFound();
            return Ok(demande);
        }

        [HttpGet("statut/{statut}")]
        public async Task<ActionResult<IEnumerable<DemandeContreVisite>>> GetByStatut(int statut)
        {
            var demandes = await _demandeContreVisiteRepository.GetByStatutAsync(statut);
            return Ok(demandes);
        }

        [HttpPost]
        public async Task<ActionResult<DemandeContreVisite>> Create(DemandeContreVisite demandeContreVisite)
        {
            await _demandeContreVisiteRepository.AddAsync(demandeContreVisite);
            await _demandeContreVisiteRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = demandeContreVisite.DemandeVisiteControleId }, demandeContreVisite);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DemandeContreVisite demandeContreVisite)
        {
            if (id != demandeContreVisite.DemandeVisiteControleId) return BadRequest();

            _demandeContreVisiteRepository.UpdateAsync(demandeContreVisite);
            await _demandeContreVisiteRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var demande = await _demandeContreVisiteRepository.GetByIdAsync(id);
            if (demande == null) return NotFound();

            await _demandeContreVisiteRepository.DeleteAsync(demande);
            await _demandeContreVisiteRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}