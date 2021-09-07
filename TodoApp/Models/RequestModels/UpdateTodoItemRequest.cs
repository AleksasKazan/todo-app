using Contracts.Enums;

namespace TodoApp.Models.RequestModels
{
    public class UpdateTodoItemRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Difficulty Difficulty { get; set; }

        public bool IsCompleted { get; set; }
    }
}
