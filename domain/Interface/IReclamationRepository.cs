using domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IReclamationRepository : IGenericRepository<Reclamation>
    {
        Task<IEnumerable<Reclamation>> GetByStatutAsync(int statut);
        Task<IEnumerable<Reclamation>> GetByTypeAsync(int type);
        Task<IEnumerable<Reclamation>> GetByAdherentAsync(int adherentId);
        Task<IEnumerable<Reclamation>> GetWithDetailsAsync();
    }
}