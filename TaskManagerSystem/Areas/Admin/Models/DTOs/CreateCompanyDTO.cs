using System.ComponentModel.DataAnnotations;

namespace TaskManagerSystem.Areas.Admin.Models.DTOs
{
    public class CreateCompanyDTO
    {
        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        public string CompanyName { get; set; }
    }
}
