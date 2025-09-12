using domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IFactureRepository : IGenericRepository<Facture>
    {
        Task<IEnumerable<Facture>> GetByStatutAsync(int statut);
        Task<IEnumerable<Facture>> GetWithPaiementsAsync();
        Task<IEnumerable<Facture>> GetByDateEmissionAsync(DateTime date);
    }
}