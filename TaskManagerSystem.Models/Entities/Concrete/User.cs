using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Enums;

namespace TaskManagerSystem.Models.Entities.Concrete
{
    public class User : IdentityUser
    {
        public User()
        {
            UserTasks=new List<UserTask>();
            ManagerUsers = new List<ManagerUser>();
            TaskDescriptions = new List<TaskDescription>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Statu Statu { get; set; } = Statu.Active;

        /*bir user'in bir company'is var*/
        public Guid CompanyID { get; set; }
        public virtual Company Companys { get; set; }

        public virtual List<UserTask> UserTasks { get; set; }
        public virtual List<ManagerUser> ManagerUsers { get; set; }
        public virtual List<TaskDescription>  TaskDescriptions { get; set; }

    }
}
