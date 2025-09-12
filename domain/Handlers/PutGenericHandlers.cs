using System.Threading.Tasks;
using domain.Commands;
using domain.Interface;

namespace domain.Handlers
{
    public class PutGenericHandlers<T> where T : class
    {
        private readonly IGenericRepository<T> _repo;
        public PutGenericHandlers(IGenericRepository<T> repo) => _repo = repo;

        public async Task<T> Handle(PutGenericCommand<T> command)
        {
            await _repo.UpdateAsync(command.Entity);
            return command.Entity;
        }
    }
}
