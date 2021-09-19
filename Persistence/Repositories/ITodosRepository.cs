using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Models.ReadModels;

namespace Persistence.Repositories
{
    public interface ITodosRepository
    {
        Task<IEnumerable<TodoItemReadModel>> GetAll();

        Task<TodoItemReadModel> Get(Guid id);

        Task<int> SaveOrUpdate(TodoItemReadModel todoItem);

        Task<int> Delete(Guid id);
    }
}
