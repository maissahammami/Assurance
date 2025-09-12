using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlansSanteController : ControllerBase
    {
        private readonly IPlanSanteRepository _planSanteRepository;

        public PlansSanteController(IPlanSanteRepository planSanteRepository)
        {
            _planSanteRepository = planSanteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanSante>>> GetAll()
        {
            var plans = await _planSanteRepository.GetAllAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanSante>> GetById(int id)
        {
            var plan = await _planSanteRepository.GetByIdAsync(id);
            if (plan == null) return NotFound();
            return Ok(plan);
        }

        [HttpGet("niveau/{niveauCouverture}")]
        public async Task<ActionResult<IEnumerable<PlanSante>>> GetByNiveauCouverture(string niveauCouverture)
        {
            var plans = await _planSanteRepository.GetByNiveauCouvertureAsync(niveauCouverture);
            return Ok(plans);
        }

        [HttpPost]
        public async Task<ActionResult<PlanSante>> Create(PlanSante planSante)
        {
            await _planSanteRepository.AddAsync(planSante);
            await _planSanteRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = planSante.PlanSanteId }, planSante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PlanSante planSante)
        {
            if (id != planSante.PlanSanteId) return BadRequest();

            _planSanteRepository.UpdateAsync(planSante);
            await _planSanteRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var plan = await _planSanteRepository.GetByIdAsync(id);
            if (plan == null) return NotFound();

            await _planSanteRepository.DeleteAsync(plan);
            await _planSanteRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}