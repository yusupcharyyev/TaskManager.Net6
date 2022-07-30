using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.Areas.Admin.Models.DTOs
{
    public class CreatePersonelDTO
    {
        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public Guid YoneticiID { get; set; }
        public List<IdentityUser> Yoneticiler { get; set; }


    }
}
