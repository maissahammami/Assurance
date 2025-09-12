using System.Collections.Generic;
using System.Threading.Tasks;
using domain.Interface;
using domain.Queries;
using MediatR;

namespace domain.Handlers
{
    public class GetListGenericHandlers<T> where T : class
    {
        private readonly IGenericRepository<T> _repo;
        public GetListGenericHandlers(IGenericRepository<T> repo) => _repo = repo;

        public async Task<IEnumerable<T>> Handle(GetListGenericQuery query)
        {
            return await _repo.GetAllAsync();
        }
    }
}
