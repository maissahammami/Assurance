using domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IRapportMedicalRepository : IGenericRepository<RapportMedical>
    {
        Task<IEnumerable<RapportMedical>> GetByMedecinAsync(int medecinId);
        Task<IEnumerable<RapportMedical>> GetByDemandeVisiteAsync(int demandeVisiteId);
        Task<IEnumerable<RapportMedical>> GetWithDetailsAsync();
    }
}