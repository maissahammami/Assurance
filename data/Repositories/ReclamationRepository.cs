using domain.Data;
using domain.Interface;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data.Repositories
{
    public class ReclamationRepository : GenericRepository<Reclamation>, IReclamationRepository
    {
        public ReclamationRepository(AssuranceDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reclamation>> GetByStatutAsync(int statut)
        {
            return await _context.Reclamations
                .Where(r => r.Statut == statut)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reclamation>> GetByTypeAsync(int type)
        {
            return await _context.Reclamations
                .Where(r => r.Type == type)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reclamation>> GetByAdherentAsync(int adherentId)
        {
            return await _context.Reclamations
                .Where(r => r.AdherentId == adherentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reclamation>> GetWithDetailsAsync()
        {
            return await _context.Reclamations
                .Include(r => r.Adherent)
                .Include(r => r.Agent)
                .Include(r => r.DemandeContreVisite)
                .ToListAsync();
        }
    }
}