using domain.Data;
using domain.Interface;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data.Repositories
{
    public class DemandeAdhesionRepository : GenericRepository<DemandeAdhesion>, IDemandeAdhesionRepository
    {
        public DemandeAdhesionRepository(AssuranceDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DemandeAdhesion>> GetByStatutAsync(int statut)
        {
            return await _context.DemandesAdhesion
                .Where(d => d.Statut == statut)
                .Include(d => d.Adherent)
                .Include(d => d.PlanSante)
                .Include(d => d.Agent)
                .ToListAsync();
        }

        public async Task<IEnumerable<DemandeAdhesion>> GetWithDetailsAsync()
        {
            return await _context.DemandesAdhesion
                .Include(d => d.Adherent)
                .Include(d => d.PlanSante)
                .Include(d => d.Agent)
                .ToListAsync();
        }
    }
}