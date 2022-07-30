using System.ComponentModel.DataAnnotations;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.Areas.Admin.Models.DTOs
{
    public class CreateManagerDTO
    {
        [Required(ErrorMessage ="Bu Alan Boş Bırakılamaz")]
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

        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        public Guid SelectCompany { get; set; }

        public List<Company> Companies { get; set; }
    }
}
