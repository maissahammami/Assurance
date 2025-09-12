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
    public class FactureRepository : GenericRepository<Facture>, IFactureRepository
    {
        public FactureRepository(AssuranceDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Facture>> GetByStatutAsync(int statut)
        {
            return await _context.Factures
                .Where(f => f.Statut == statut)
                .ToListAsync();
        }

        public async Task<IEnumerable<Facture>> GetWithPaiementsAsync()
        {
            return await _context.Factures
                .Include(f => f.Paiements)
                .Include(f => f.Cotisation)
                .ThenInclude(c => c.Adherent)
                .ToListAsync();
        }

        public async Task<IEnumerable<Facture>> GetByDateEmissionAsync(DateTime date)
        {
            return await _context.Factures
                .Where(f => f.DateEmission.Date == date.Date)
                .ToListAsync();
        }
    }
}