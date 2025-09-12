using domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Interface
{
    public interface IMedecinControleRepository : IGenericRepository<MedecinControle>
    {
        Task<MedecinControle> GetByNumeroLicenceAsync(string numeroLicence);
        Task<IEnumerable<MedecinControle>> GetBySpecialiteAsync(string specialite);
        Task<IEnumerable<MedecinControle>> GetWithDemandesAsync();
    }
}