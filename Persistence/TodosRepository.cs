using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;

namespace Persistence
{
    public class TodosRepository : ITodosRepository
    {
        private const string TodosTable = "Todos";
        private readonly ISqlClient _sqlClient;

        public TodosRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<IEnumerable<TodoItemReadModel>> GetAll()
        {
            var sql = @$"SELECT Id, Title, Description, Difficulty, DateCreated, IsComplete FROM {TodosTable}";
            return _sqlClient.QueryAsync<TodoItemReadModel>(sql);
        }

        public Task<TodoItemReadModel> Get(Guid id)
        {
            var sql = @$"SELECT Id, Title, Description, Difficulty, DateCreated, IsComplete FROM {TodosTable}
                        WHERE Id = @Id";
            return _sqlClient.QuerySingleOrDefaultAsync<TodoItemReadModel>(sql, new
            {
                Id = id
            });
        }

        public Task<int> SaveOrUpdate(TodoItemWriteModel todoItem)
        {
            var sql = @$"INSERT INTO {TodosTable} (Id, Title, Description, Difficulty, DateCreated, IsComplete)
                        VALUES(@Id, @Title, @Description, @Difficulty, @DateCreated, @IsComplete)
                        ON DUPLICATE KEY UPDATE Title = @Title, Description = @Description, Difficulty = @Difficulty, IsComplete = @IsComplete";

            return _sqlClient.ExecuteAsync(sql, new
            {
                todoItem.Id,
                todoItem.Title,
                todoItem.Description,
                Difficulty = todoItem.Difficulty.ToString(),
                todoItem.DateCreated,
                todoItem.IsComplete
            });
        }

        public Task<int> Delete(Guid id)
        {
            var sql = @$"DELETE FROM {TodosTable} WHERE Id = @Id";

            return _sqlClient.ExecuteAsync(sql, new
            {
                Id = id
            });
        }
    }
}
