using domain.Data;
using domain.Interface;
using domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data.Repositories
{
    public class MedecinControleRepository : GenericRepository<MedecinControle>, IMedecinControleRepository
    {
        public MedecinControleRepository(AssuranceDbContext context) : base(context)
        {
        }

        public Task<MedecinControle> GetByNumeroLicenceAsync(string numeroLicence)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MedecinControle>> GetBySpecialiteAsync(string specialite)
        {
            throw new System.NotImplementedException();
        }

        //public async Task<MedecinControle> GetByNumeroLicenceAsync(string numeroLicence)
        //{
        //    return await _context.MedecinsControle
        //        .FirstOrDefaultAsync(m => m.NumeroLicence == numeroLicence);
        //}

        //public async Task<IEnumerable<MedecinControle>> GetBySpecialiteAsync(string specialite)
        //{
        //    return await _context.MedecinsControle
        //        .Where(m => m.Specialite == specialite)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<MedecinControle>> GetWithDemandesAsync()
        {
            return await _context.MedecinsControle
                .Include(m => m.DemandeContreVisites)
                .Include(m => m.RapportsMedical)
                .ToListAsync();
        }
    }
}