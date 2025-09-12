using domain.Data;
using domain.Interface;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data.Repositories
{
    public class DemandeContreVisiteRepository : GenericRepository<DemandeContreVisite>, IDemandeContreVisiteRepository
    {
        public DemandeContreVisiteRepository(AssuranceDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DemandeContreVisite>> GetByStatutAsync(int statut)
        {
            return await _context.DemandesContreVisite
                .Where(d => d.Statut == statut)
                .ToListAsync();
        }

        public async Task<IEnumerable<DemandeContreVisite>> GetWithDetailsAsync()
        {
            return await _context.DemandesContreVisite
                .Include(d => d.MedecinControle)
                .Include(d => d.RapportMedical)
                .Include(d => d.Reclamations)
                .ToListAsync();
        }

        public async Task<IEnumerable<DemandeContreVisite>> GetByMedecinAsync(int medecinId)
        {
            return await _context.DemandesContreVisite
                .Where(d => d.ControleurMedicalId == medecinId)
                .ToListAsync();
        }
    }
}