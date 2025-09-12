using domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IPaiementRepository : IGenericRepository<Paiement>
    {
        Task<IEnumerable<Paiement>> GetByAdherentAsync(int adherentId);
        Task<IEnumerable<Paiement>> GetByFactureAsync(int factureId);
        Task<IEnumerable<Paiement>> GetByDatePaiementAsync(DateTime date);
    }
}