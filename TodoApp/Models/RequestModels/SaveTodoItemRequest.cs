using Contracts.Enums;

namespace TodoApp.Models.RequestModels
{
    public class SaveTodoItemRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Difficulty Difficulty { get; set; }
    }
}
