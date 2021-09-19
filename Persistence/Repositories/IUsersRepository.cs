using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Models.ReadModels;

namespace Persistence.Repositories
{
    public interface IUsersRepository
    {
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Returns all users Objects list</returns>
        Task<IEnumerable<UserReadModel>> GetAllUsers();

        Task<IEnumerable<ApiKeyReadModel>> GetAllApiKeys(Guid id);

        Task<UserReadModel> GetUserById(Guid id);

        /// <summary>
        /// Saves or Updates user
        /// </summary>
        /// <returns>Returns user Object</returns>
        Task<int> CreateUser(UserReadModel user);

        /// <summary>
        /// Saves or Updates ApiKey
        /// </summary>
        /// <returns>Returns ApiKey Object</returns>
        Task<int> CreateApiKey(ApiKeyReadModel apiKey);

        Task<UserReadModel> GetUser(string userName, string password);

        Task<UserReadModel> GetUserByName(string userName);

        Task<ApiKeyReadModel> GetApiKey(string apiKey);

        Task<ApiKeyReadModel> GetApiKeyByApiKeyId(Guid apiKeyId);
    }
}
