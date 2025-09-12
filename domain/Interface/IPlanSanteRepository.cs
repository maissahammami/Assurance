using domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IPlanSanteRepository : IGenericRepository<PlanSante>
    {
        Task<PlanSante> GetByCodeAsync(string code);
        Task<IEnumerable<PlanSante>> GetByNiveauCouvertureAsync(string niveauCouverture);
        Task<IEnumerable<PlanSante>> GetWithDemandesAsync();
    }
}