using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RapportsMedicalController : ControllerBase
    {
        private readonly IRapportMedicalRepository _rapportMedicalRepository;

        public RapportsMedicalController(IRapportMedicalRepository rapportMedicalRepository)
        {
            _rapportMedicalRepository = rapportMedicalRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RapportMedical>>> GetAll()
        {
            var rapports = await _rapportMedicalRepository.GetAllAsync();
            return Ok(rapports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RapportMedical>> GetById(int id)
        {
            var rapport = await _rapportMedicalRepository.GetByIdAsync(id);
            if (rapport == null) return NotFound();
            return Ok(rapport);
        }

        [HttpGet("medecin/{medecinId}")]
        public async Task<ActionResult<IEnumerable<RapportMedical>>> GetByMedecin(int medecinId)
        {
            var rapports = await _rapportMedicalRepository.GetByMedecinAsync(medecinId);
            return Ok(rapports);
        }

        [HttpPost]
        public async Task<ActionResult<RapportMedical>> Create(RapportMedical rapportMedical)
        {
            await _rapportMedicalRepository.AddAsync(rapportMedical);
            await _rapportMedicalRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = rapportMedical.RapportMedicalId }, rapportMedical);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RapportMedical rapportMedical)
        {
            if (id != rapportMedical.RapportMedicalId) return BadRequest();

            _rapportMedicalRepository.UpdateAsync(rapportMedical);
            await _rapportMedicalRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rapport = await _rapportMedicalRepository.GetByIdAsync(id);
            if (rapport == null) return NotFound();

            await _rapportMedicalRepository.DeleteAsync(rapport);
            await _rapportMedicalRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}