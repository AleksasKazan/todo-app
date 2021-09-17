using System;
namespace TodoApp.Models.ResponseModels
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
