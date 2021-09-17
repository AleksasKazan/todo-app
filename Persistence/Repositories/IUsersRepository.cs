using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Models.ReadModels;

namespace Persistence.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UserReadModel>> GetAllUsers();

        Task<IEnumerable<ApiKeyReadModel>> GetAllApiKeys(Guid id);

        Task<UserReadModel> GetUserById(Guid id);

        Task<int> CreateUser(UserReadModel user);

        Task<int> CreateApiKey(ApiKeyReadModel apiKey);

        Task<ApiKeyReadModel> GetApiKey(string apiKey);

        Task<UserReadModel> GetUser(string userName, string password);

    }
}
