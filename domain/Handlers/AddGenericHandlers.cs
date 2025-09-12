using domain.Commands;
using domain.Interface;
using System.Threading.Tasks;

namespace domain.Handlers
{
    public class AddGenericHandlers<T> where T : class
    {
        private readonly IGenericRepository<T> _repo;
        public AddGenericHandlers(IGenericRepository<T> repo) => _repo = repo;

        public async Task<T> Handle(AddGenericCommand<T> command)
        {
            await _repo.AddAsync(command.Entity);
            return command.Entity;
        }
    }
}

