using domain.Data;
using domain.Interface;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data.Repositories
{
    public class RapportMedicalRepository : GenericRepository<RapportMedical>, IRapportMedicalRepository
    {
        public RapportMedicalRepository(AssuranceDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RapportMedical>> GetByMedecinAsync(int medecinId)
        {
            return await _context.RapportsMedical
                .Where(r => r.ControleurMedicalId == medecinId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RapportMedical>> GetByDemandeVisiteAsync(int demandeVisiteId)
        {
            return await _context.RapportsMedical
                .Where(r => r.DemandeVisiteControleId == demandeVisiteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RapportMedical>> GetWithDetailsAsync()
        {
            return await _context.RapportsMedical
                .Include(r => r.MedecinControle)
                .Include(r => r.DemandeContreVisite)
                .ToListAsync();
        }
    }
}