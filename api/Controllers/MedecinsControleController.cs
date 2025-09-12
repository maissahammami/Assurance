using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedecinsControleController : ControllerBase
    {
        private readonly IMedecinControleRepository _medecinControleRepository;

        public MedecinsControleController(IMedecinControleRepository medecinControleRepository)
        {
            _medecinControleRepository = medecinControleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedecinControle>>> GetAll()
        {
            var medecins = await _medecinControleRepository.GetAllAsync();
            return Ok(medecins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedecinControle>> GetById(int id)
        {
            var medecin = await _medecinControleRepository.GetByIdAsync(id);
            if (medecin == null) return NotFound();
            return Ok(medecin);
        }

        [HttpGet("specialite/{specialite}")]
        public async Task<ActionResult<IEnumerable<MedecinControle>>> GetBySpecialite(string specialite)
        {
            var medecins = await _medecinControleRepository.GetBySpecialiteAsync(specialite);
            return Ok(medecins);
        }

        [HttpPost]
        public async Task<ActionResult<MedecinControle>> Create(MedecinControle medecinControle)
        {
            await _medecinControleRepository.AddAsync(medecinControle);
            await _medecinControleRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = medecinControle.ControleurMedicalId }, medecinControle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedecinControle medecinControle)
        {
            if (id != medecinControle.ControleurMedicalId) return BadRequest();

            _medecinControleRepository.UpdateAsync(medecinControle);
            await _medecinControleRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var medecin = await _medecinControleRepository.GetByIdAsync(id);
            if (medecin == null) return NotFound();

            await _medecinControleRepository.DeleteAsync(medecin);
            await _medecinControleRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}