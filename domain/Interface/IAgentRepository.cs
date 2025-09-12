using domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IAgentRepository : IGenericRepository<Agent>
    {
        Task<Agent> GetByEmailAsync(string email);
        Task<IEnumerable<Agent>> GetByRoleAsync(int role);
        Task<IEnumerable<Agent>> GetWithDemandesAsync();
    }
}