using domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface ICotisationRepository : IGenericRepository<Cotisation>
    {
        Task<IEnumerable<Cotisation>> GetByAdherentAsync(int adherentId);
        Task<IEnumerable<Cotisation>> GetWithDetailsAsync();
        Task<IEnumerable<Cotisation>> GetByPeriodeAsync(DateTime periode);
    }
}