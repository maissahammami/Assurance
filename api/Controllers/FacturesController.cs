using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturesController : ControllerBase
    {
        private readonly IFactureRepository _factureRepository;

        public FacturesController(IFactureRepository factureRepository)
        {
            _factureRepository = factureRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facture>>> GetAll()
        {
            var factures = await _factureRepository.GetAllAsync();
            return Ok(factures);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Facture>> GetById(int id)
        {
            var facture = await _factureRepository.GetByIdAsync(id);
            if (facture == null) return NotFound();
            return Ok(facture);
        }

        [HttpGet("statut/{statut}")]
        public async Task<ActionResult<IEnumerable<Facture>>> GetByStatut(int statut)
        {
            var factures = await _factureRepository.GetByStatutAsync(statut);
            return Ok(factures);
        }

        [HttpPost]
        public async Task<ActionResult<Facture>> Create(Facture facture)
        {
            await _factureRepository.AddAsync(facture);
            await _factureRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = facture.FactureId }, facture);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Facture facture)
        {
            if (id != facture.FactureId) return BadRequest();

            _factureRepository.UpdateAsync(facture);
            await _factureRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var facture = await _factureRepository.GetByIdAsync(id);
            if (facture == null) return NotFound();

            await _factureRepository.DeleteAsync(facture);
            await _factureRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}