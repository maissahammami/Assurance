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
    public class PaiementRepository : GenericRepository<Paiement>, IPaiementRepository
    {
        public PaiementRepository(AssuranceDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Paiement>> GetByAdherentAsync(int adherentId)
        {
            return await _context.Paiements
                .Where(p => p.AdherentId == adherentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Paiement>> GetByFactureAsync(int factureId)
        {
            return await _context.Paiements
                .Where(p => p.FactureId == factureId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Paiement>> GetByDatePaiementAsync(DateTime date)
        {
            return await _context.Paiements
                .Where(p => p.DatePaiement.Date == date.Date)
                .ToListAsync();
        }
    }
}