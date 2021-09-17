using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Persistence.Models.ReadModels;
using Persistence.Repositories;
using TodoApp.Models.RequestModels;
using TodoApp.Models.ResponseModels;
using TodoApp.Options;

namespace TodoApp.Controllers
{
    [ApiController]
    public class UsersControler : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        public UsersControler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetUsers()
        {
            //var userId = (Guid)HttpContext.Items["userId"];
            var users = await _usersRepository.GetAllUsers();

            return new ActionResult<IEnumerable<UserResponseModel>>(users.Select(user => user.MapToUserResponse()));
        }

        [HttpGet]
        [Route("users/{id}")]
        public async Task<ActionResult<UserResponseModel>> GetUserById(Guid id)
        {
            var user = await _usersRepository.GetUserById(id);
            if (user is null)
            {
                return NotFound($"User with id: '{id}' does not exist");
            }
            return user.MapToUserResponse();
        }

        [HttpGet]
        [Route("users/{apiKey}/apiObj")]
        public async Task<ActionResult<ApiKeyResponseModel>> GetApiObjByApiKey(string apiKey)
        {
            var apiKeyObj = await _usersRepository.GetApiKey(apiKey);
            if (apiKeyObj is null)
            {
                return NotFound($"ApiKey with id: '{apiKey}' does not exist");
            }
            return apiKeyObj.MapToApiKeyResponse();
        }

        [HttpGet]
        [Route("users/{id}/apiKeys")]
        public async Task<ActionResult<IEnumerable<ApiKeyResponseModel>>> GetApiKeysByUserId(Guid id)
        {
            //var user = await _usersRepository.GetUserById(id);
            //if (user is null)
            //{
            //    return NotFound($"User with id: '{id}' does not exist");
            //}
            var apiKeys = await _usersRepository.GetAllApiKeys(id);
            if (apiKeys is null)
            {
                return NotFound($"ApiKey with id: '{id}' does not exist");
            }

            return new ActionResult<IEnumerable<ApiKeyResponseModel>>(apiKeys.Select(apiKey => apiKey.MapToApiKeyResponse()));
        }

        [HttpPost]
        [Route("users/signUp")]
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> SignUp([FromBody] SaveUserRequest request)
        {
            //var userId = (Guid)HttpContext.Items["userId"];
            var user = new UserReadModel
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Password = request.Password,
                DateCreated = DateTime.Now,
            };
            await _usersRepository.CreateUser(user);

            return CreatedAtAction("GetUserById", new { user.Id }, user.MapToUserResponse());
        }

        [HttpPost]
        [Route("users/apiKey")]
        public async Task<ActionResult<ApiKeyResponseModel>> GenerateApiKey([FromBody] SaveApiKeyRequest request)
        {
            var user = await _usersRepository.GetUser(request.UserName, request.Password);
            var apiKey = new ApiKeyReadModel
            {
                Id = Guid.NewGuid(),
                ApiKey = Guid.NewGuid().ToString("N"),
                UserId = user.Id,
                IsActive = true,
                DateCreated = DateTime.Now
            };
            await _usersRepository.CreateApiKey(apiKey);

            return CreatedAtAction("GetApiKeysByUserId", new { user.Id }, apiKey.MapToApiKeyResponse());
        }
    }
}
