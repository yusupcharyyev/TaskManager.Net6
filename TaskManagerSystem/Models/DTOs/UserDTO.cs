using System.ComponentModel.DataAnnotations;

namespace TaskManagerSystem.Models.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}
