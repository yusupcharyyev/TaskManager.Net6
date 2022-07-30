using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Abstract;
using TaskManagerSystem.Models.Enums;

namespace TaskManagerSystem.Models.Entities.Concrete
{
    public class Tasks : BaseEntity
    {
        public Tasks()
        {
            TaskDescriptions = new List<TaskDescription>();
            UserTasks = new List<UserTask>();
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public int EndTime { get; set; }
        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public PriorityStatu priorityStatu { get; set; }
        public Enums.Action Action { get; set; }


        /*Açıklamalar*/
        public virtual List<TaskDescription> TaskDescriptions { get; set; }

        /*Eklenen Personeller/Yoneticiler*/
        public virtual List<UserTask> UserTasks { get; set; }

        /*Oluşturan Kişi*/
        public Guid UserID { get; set; }
    }
}
