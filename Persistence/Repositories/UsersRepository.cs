using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Models.ReadModels;

namespace Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private const string UsersTable = "Users";
        private const string ApiKeysTable = "ApiKeys";
        private readonly ISqlClient _sqlClient;

        public UsersRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<IEnumerable<UserReadModel>> GetAllUsers()
        {
            var sql = $"SELECT * FROM {UsersTable}";
            return _sqlClient.QueryAsync<UserReadModel>(sql);
        }

        public Task<ApiKeyReadModel> GetApiKey(string apiKey)
        {
            var sql = @$"SELECT * FROM {ApiKeysTable} WHERE ApiKey = @ApiKey";
            return _sqlClient.QuerySingleOrDefaultAsync<ApiKeyReadModel>(sql, new
            {
                ApiKey = apiKey
            });
        }

        public Task<IEnumerable<ApiKeyReadModel>> GetAllApiKeys(Guid id)
        {
            var sql = @$"SELECT * FROM {ApiKeysTable} WHERE UserId = @Id";
            return _sqlClient.QueryAsync<ApiKeyReadModel>(sql, new
            {
                Id = id
            });
        }

        public Task<UserReadModel> GetUserById(Guid id)
        {
            var sql = $"SELECT * FROM {UsersTable} WHERE Id = @Id";
            return _sqlClient.QuerySingleOrDefaultAsync<UserReadModel>(sql, new
            {
                Id = id
            });
        }

        public Task<UserReadModel> GetUser(string userName, string password)
        {
            var sql = @$"SELECT * FROM {UsersTable} WHERE UserName = @UserName AND Password = @Password";

            return _sqlClient.QuerySingleOrDefaultAsync<UserReadModel>(sql, new
            {
                UserName = userName,
                Password = password
            });
        }

        public Task<int> CreateUser(UserReadModel user)
        {
            var sql = @$"INSERT INTO {UsersTable} (Id, UserName, Password, DateCreated)
                        VALUES(@Id, @UserName, @Password, @DateCreated)
            ON DUPLICATE KEY UPDATE UserName = @UserName, Password = @Password";

            return _sqlClient.ExecuteAsync(sql, new
            {
                user.Id,
                user.UserName,
                user.Password,
                user.DateCreated
            });
        }

        public Task<int> CreateApiKey(ApiKeyReadModel apiKey)
        {
            var sql = @$"INSERT INTO {ApiKeysTable} (Id, ApiKey, UserId, IsActive, DateCreated)
                        VALUES(@Id, @ApiKey, @UserId, @IsActive, @DateCreated)
            ON DUPLICATE KEY UPDATE Id = @Id, ApiKey = @ApiKey, UserId = @UserId, IsActive = @IsActive, DateCreated = @DateCreated";

            return _sqlClient.ExecuteAsync(sql, new
            {
                apiKey.Id,
                apiKey.ApiKey,
                apiKey.UserId,
                apiKey.IsActive,
                apiKey.DateCreated

            });
        }        
    }
}
