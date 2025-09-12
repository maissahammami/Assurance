using domain.Data;
using domain.Interface;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data.Repositories
{
    public class AdherentRepository : GenericRepository<Adherent>, IAdherentRepository
    {
        public AdherentRepository(AssuranceDbContext context) : base(context)
        {
        }

        public Task<Adherent> GetByMatriculeAsync(string matricule)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Adherent>> GetByStatutAsync(int statut)
        {
            throw new System.NotImplementedException();
        }

        //public async Task<Adherent> GetByMatriculeAsync(string matricule)
        //{
        //    return await _context.Adherents
        //        .FirstOrDefaultAsync(a => a.Matricule == matricule);
        //}

        //public async Task<IEnumerable<Adherent>> GetByStatutAsync(int statut)
        //{
        //    return await _context.Adherents
        //        .Where(a => a.Statut == statut)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Adherent>> GetWithDemandesAsync()
        {
            return await _context.Adherents
                .Include(a => a.DemandesAdhesion)
                .ToListAsync();
        }
    }
}