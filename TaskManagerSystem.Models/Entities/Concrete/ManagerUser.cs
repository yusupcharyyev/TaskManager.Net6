using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerSystem.Models.Entities.Concrete
{
    public class ManagerUser
    {
        public Guid UserId { get; set; }
        public virtual User Users { get; set; }

        public Guid ManagerId { get; set; }
        [NotMapped]
        public virtual User Manager { get; set; }
    }
}
