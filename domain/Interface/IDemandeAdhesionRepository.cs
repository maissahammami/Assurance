using domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IDemandeAdhesionRepository : IGenericRepository<DemandeAdhesion>
    {
        Task<IEnumerable<DemandeAdhesion>> GetByStatutAsync(int statut);
        Task<IEnumerable<DemandeAdhesion>> GetWithDetailsAsync();
    }
}