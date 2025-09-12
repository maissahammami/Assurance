using System;
using System.Collections.Generic;
using System.Text;

using MediatR;
using domain.Interface;
using domain.Queries;
using System.Threading.Tasks;
using System.Threading;

namespace domain.Handlers
{
    public class GetByIdGenericHandlers<T> : IRequestHandler<GetByIdGenericQuery<T>, T> where T : class
    {
        private readonly IGenericRepository<T> _repo;
        public GetByIdGenericHandlers(IGenericRepository<T> repo) => _repo = repo;

        public async Task<T> Handle(GetByIdGenericQuery<T> request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
