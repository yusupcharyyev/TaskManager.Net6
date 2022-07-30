using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerSystem.Models.Entities.Concrete;
using TaskManagerSystem.Models.Enums;

namespace TaskManagerSystem.Areas.Manager.Models.VMs
{
    public class CreateTaskVM
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

        public Guid SelectPersonel { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
