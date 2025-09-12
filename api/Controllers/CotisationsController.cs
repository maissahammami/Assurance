using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotisationsController : ControllerBase
    {
        private readonly ICotisationRepository _cotisationRepository;

        public CotisationsController(ICotisationRepository cotisationRepository)
        {
            _cotisationRepository = cotisationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cotisation>>> GetAll()
        {
            var cotisations = await _cotisationRepository.GetAllAsync();
            return Ok(cotisations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cotisation>> GetById(int id)
        {
            var cotisation = await _cotisationRepository.GetByIdAsync(id);
            if (cotisation == null) return NotFound();
            return Ok(cotisation);
        }

        [HttpGet("adherent/{adherentId}")]
        public async Task<ActionResult<IEnumerable<Cotisation>>> GetByAdherent(int adherentId)
        {
            var cotisations = await _cotisationRepository.GetByAdherentAsync(adherentId);
            return Ok(cotisations);
        }

        [HttpPost]
        public async Task<ActionResult<Cotisation>> Create(Cotisation cotisation)
        {
            await _cotisationRepository.AddAsync(cotisation);
            await _cotisationRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = cotisation.CotisationId }, cotisation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cotisation cotisation)
        {
            if (id != cotisation.CotisationId) return BadRequest();

            _cotisationRepository.UpdateAsync(cotisation);
            await _cotisationRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cotisation = await _cotisationRepository.GetByIdAsync(id);
            if (cotisation == null) return NotFound();

            await _cotisationRepository.DeleteAsync(cotisation);
            await _cotisationRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}