using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaiementsController : ControllerBase
    {
        private readonly IPaiementRepository _paiementRepository;

        public PaiementsController(IPaiementRepository paiementRepository)
        {
            _paiementRepository = paiementRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paiement>>> GetAll()
        {
            var paiements = await _paiementRepository.GetAllAsync();
            return Ok(paiements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paiement>> GetById(int id)
        {
            var paiement = await _paiementRepository.GetByIdAsync(id);
            if (paiement == null) return NotFound();
            return Ok(paiement);
        }

        [HttpGet("adherent/{adherentId}")]
        public async Task<ActionResult<IEnumerable<Paiement>>> GetByAdherent(int adherentId)
        {
            var paiements = await _paiementRepository.GetByAdherentAsync(adherentId);
            return Ok(paiements);
        }

        [HttpPost]
        public async Task<ActionResult<Paiement>> Create(Paiement paiement)
        {
            await _paiementRepository.AddAsync(paiement);
            await _paiementRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = paiement.PaiementId }, paiement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Paiement paiement)
        {
            if (id != paiement.PaiementId) return BadRequest();

            _paiementRepository.UpdateAsync(paiement);
            await _paiementRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paiement = await _paiementRepository.GetByIdAsync(id);
            if (paiement == null) return NotFound();

            await _paiementRepository.DeleteAsync(paiement);
            await _paiementRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}