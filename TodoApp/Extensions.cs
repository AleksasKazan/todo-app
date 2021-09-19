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
                IsCompleted = todoItem.IsCompleted,
                UserId = todoItem.UserId
            };
        }

        public static UserResponseModel MapToUserResponse(this UserReadModel user)
        {
            return new UserResponseModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                DateCreated = user.DateCreated
            };
        }

        public static ApiKeyResponseModel MapToApiKeyResponse(this ApiKeyReadModel apiKey)
        {
            return new ApiKeyResponseModel
            {
                Id = apiKey.Id,
                ApiKey = apiKey.ApiKey,
                UserId = apiKey.UserId,
                IsActive = apiKey.IsActive,
                DateCreated = apiKey.DateCreated,
                ExpirationDate = apiKey.ExpirationDate
            };
        }
    }
}
