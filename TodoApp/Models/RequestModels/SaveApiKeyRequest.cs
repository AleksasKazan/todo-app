using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.RequestModels
{
    public class SaveApiKeyRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
