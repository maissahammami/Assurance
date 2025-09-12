using domain.Data;
using domain.Interface;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data.Repositories
{
    public class CotisationRepository : GenericRepository<Cotisation>, ICotisationRepository
    {
        public CotisationRepository(AssuranceDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cotisation>> GetByAdherentAsync(int adherentId)
        {
            return await _context.Cotisations
                .Where(c => c.AdherentId == adherentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cotisation>> GetWithDetailsAsync()
        {
            return await _context.Cotisations
                .Include(c => c.Adherent)
                .Include(c => c.DemandeAdhesion)
                .ThenInclude(da => da.PlanSante)
                .Include(c => c.Facture)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cotisation>> GetByPeriodeAsync(DateTime periode)
        {
            return await _context.Cotisations
                .Where(c => c.Periode == periode)
                .ToListAsync();
        }
    }
}