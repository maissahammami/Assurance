using domain.Data;
using domain.Interface;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data.Repositories
{
    public class PlanSanteRepository : GenericRepository<PlanSante>, IPlanSanteRepository
    {
        public PlanSanteRepository(AssuranceDbContext context) : base(context)
        {
        }

        public Task<PlanSante> GetByCodeAsync(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PlanSante>> GetByNiveauCouvertureAsync(string niveauCouverture)
        {
            throw new System.NotImplementedException();
        }

        //public async Task<PlanSante> GetByCodeAsync(string code)
        //{
        //    return await _context.PlansSante
        //        .FirstOrDefaultAsync(p => p.Code == code);
        //}

        //public async Task<IEnumerable<PlanSante>> GetByNiveauCouvertureAsync(string niveauCouverture)
        //{
        //    return await _context.PlansSante
        //        .Where(p => p.NiveauCouverture == niveauCouverture)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<PlanSante>> GetWithDemandesAsync()
        {
            return await _context.PlansSante
                .Include(p => p.DemandesAdhesion)
                //.Include(p => p.Cotisations)
                .ToListAsync();
        }
    }
}