using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;

namespace Persistence
{
    public interface ITodosRepository
    {
        Task<IEnumerable<TodoItemReadModel>> GetAll();

        Task<TodoItemReadModel> Get(Guid id);

        Task<int> SaveOrUpdate(TodoItemWriteModel todoItem);

        Task<int> Delete(Guid id);
    }
}
