using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerSystem.Models.Enums;

namespace TaskManagerSystem.Areas.Personel.Models.VMs
{
    public class CreateTaskPersonelVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public int EndTime { get; set; }
        public TaskManagerSystem.Models.Enums.Action Action { get; set; }
        public PriorityStatu PriorityStatu { get; set; }
        public string FilePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
