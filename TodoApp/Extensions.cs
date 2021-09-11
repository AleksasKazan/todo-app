using Persistence.Models.ReadModels;
using TodoApp.Models.ResponseModels;

namespace TodoApp
{
    public static class Extensions
    {
        public static TodoItemResponse MapToTodoItemResponse(this TodoItemReadModel todoItem)
        {
            return new TodoItemResponse
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                Difficulty = todoItem.Difficulty,
                DateCreated = todoItem.DateCreated,
                IsCompleted = todoItem.IsCompleted
            };
        }
    }
}
