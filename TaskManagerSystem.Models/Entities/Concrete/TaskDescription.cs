using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Abstract;

namespace TaskManagerSystem.Models.Entities.Concrete
{
    public class TaskDescription : BaseEntity
    {
        public string Description { get; set; }

        public Guid UserId { get; set; }
        public virtual User Users { get; set; }

        public Guid TaskId { get; set; }
        public virtual Tasks Taskss { get; set; }
    }
}
