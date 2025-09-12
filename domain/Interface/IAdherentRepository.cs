using domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IAdherentRepository : IGenericRepository<Adherent>
    {
        Task<Adherent> GetByMatriculeAsync(string matricule);
        Task<IEnumerable<Adherent>> GetByStatutAsync(int statut);
        Task<IEnumerable<Adherent>> GetWithDemandesAsync();
    }
}