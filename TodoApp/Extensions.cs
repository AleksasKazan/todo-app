using Persistence.Models.WriteModels;
using TodoApp.Models.ResponseModels;

namespace TodoApp
{
    public static class Extensions
    {
        public static TodoItemResponse MapToTodoItemResponse(this TodoItemWriteModel todoItem)
        {
            return new TodoItemResponse
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                Difficulty = todoItem.Difficulty,
                DateCreated = todoItem.DateCreated,
                IsComplete = todoItem.IsComplete
            };
        }
    }
}
