using System;
using System.Threading.Tasks;
using domain.Commands;
using domain.Interface;

namespace domain.Handlers
{
    public class DeleteGenericHandlers<T> where T : class
    {
        private readonly IGenericRepository<T> _repo;

        public DeleteGenericHandlers(IGenericRepository<T> repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteGenericCommand command)
        {
            // Fetch the entity by ID
            var entity = await _repo.GetByIdAsync(command.Id);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with ID {command.Id} not found.");
            }

            // Delete the entity
            await _repo.DeleteAsync(entity);
            await _repo.SaveChangesAsync();
        }
    }

}
