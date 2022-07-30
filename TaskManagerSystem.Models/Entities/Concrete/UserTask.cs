using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerSystem.Models.Entities.Concrete
{
    public class UserTask
    {
        public Guid UserId { get; set; }
        public virtual User Users { get; set; }

        public Guid TaskID { get; set; }
        public virtual Tasks Taskss { get; set; }
    }
}
