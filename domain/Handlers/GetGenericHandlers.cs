using System.Threading.Tasks;
using domain.Interface;
using domain.Queries;

namespace domain.Handlers
{
    public class GetGenericHandlers<T> where T : class
    {
        private readonly IGenericRepository<T> _repo;
        public GetGenericHandlers(IGenericRepository<T> repo) => _repo = repo;

        public async Task<T> Handle(GetGenericQuery query)
        {
            return await _repo.GetByIdAsync(query.Id);
        }
    }
}
