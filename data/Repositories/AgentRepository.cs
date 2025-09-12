using domain.Data;
using domain.Interface;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data.Repositories
{
    public class AgentRepository : GenericRepository<Agent>, IAgentRepository
    {
        public AgentRepository(AssuranceDbContext context) : base(context)
        {
        }

        public Task<Agent> GetByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Agent>> GetByRoleAsync(int role)
        {
            throw new System.NotImplementedException();
        }

        //public async Task<Agent> GetByEmailAsync(string email)
        //{
        //    return await _context.Agents
        //        .FirstOrDefaultAsync(a => a.Email == email);
        //}

        //public async Task<IEnumerable<Agent>> GetByRoleAsync(int role)
        //{
        //    return await _context.Agents
        //        .Where(a => a.Role == role)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Agent>> GetWithDemandesAsync()
        {
            return await _context.Agents
                .Include(a => a.DemandesAdhesion)
                .Include(a => a.Reclamations)
                .ToListAsync();
        }
    }
}