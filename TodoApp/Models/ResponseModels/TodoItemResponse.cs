using System;
using Contracts.Enums;

namespace TodoApp.Models.ResponseModels
{
    public class TodoItemResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Difficulty Difficulty { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsCompleted { get; set; }
    }
}
