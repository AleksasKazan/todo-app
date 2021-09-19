using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.RequestModels
{
    public class SaveUserRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
