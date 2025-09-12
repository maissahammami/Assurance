using Microsoft.AspNetCore.Mvc;
using domain.Models;
using domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentRepository _agentRepository;

        public AgentsController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAll()
        {
            var agents = await _agentRepository.GetAllAsync();
            return Ok(agents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agent>> GetById(int id)
        {
            var agent = await _agentRepository.GetByIdAsync(id);
            if (agent == null) return NotFound();
            return Ok(agent);
        }

        [HttpPost]
        public async Task<ActionResult<Agent>> Create(Agent agent)
        {
            await _agentRepository.AddAsync(agent);
            await _agentRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = agent.AgentId }, agent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Agent agent)
        {
            if (id != agent.AgentId) return BadRequest();

            _agentRepository.UpdateAsync(agent);
            await _agentRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var agent = await _agentRepository.GetByIdAsync(id);
            if (agent == null) return NotFound();

            await _agentRepository.DeleteAsync(agent);
            await _agentRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}