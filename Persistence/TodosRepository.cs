using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Models.ReadModels;

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
            var sql = $"SELECT * FROM {TodosTable}";
            return _sqlClient.QueryAsync<TodoItemReadModel>(sql);
        }

        public Task<TodoItemReadModel> Get(Guid id)
        {
            var sql = $"SELECT * FROM {TodosTable} WHERE Id = @Id";
            return _sqlClient.QuerySingleOrDefaultAsync<TodoItemReadModel>(sql, new
            {
                Id = id
            });
        }

        public Task<int> SaveOrUpdate(TodoItemReadModel todoItem)
        {
            var sql = @$"INSERT INTO {TodosTable} (Id, Title, Description, Difficulty, DateCreated, IsCompleted)
                        VALUES(@Id, @Title, @Description, @Difficulty, @DateCreated, @IsCompleted)
                        ON DUPLICATE KEY UPDATE Title = @Title, Description = @Description, Difficulty = @Difficulty, IsCompleted = @IsCompleted";

            return _sqlClient.ExecuteAsync(sql, new
            {
                todoItem.Id,
                todoItem.Title,
                todoItem.Description,
                Difficulty = todoItem.Difficulty.ToString(),
                todoItem.DateCreated,
                todoItem.IsCompleted
            });
        }

        public Task<int> Delete(Guid id)
        {
            var sql = $"DELETE FROM {TodosTable} WHERE Id = @Id";

            return _sqlClient.ExecuteAsync(sql, new
            {
                Id = id
            });
        }
    }
}
