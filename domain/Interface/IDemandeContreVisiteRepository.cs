using domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IDemandeContreVisiteRepository : IGenericRepository<DemandeContreVisite>
    {
        Task<IEnumerable<DemandeContreVisite>> GetByStatutAsync(int statut);
        Task<IEnumerable<DemandeContreVisite>> GetWithDetailsAsync();
        Task<IEnumerable<DemandeContreVisite>> GetByMedecinAsync(int medecinId);
    }
}