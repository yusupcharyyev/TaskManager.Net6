using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Abstract;

namespace TaskManagerSystem.Models.Entities.Concrete
{
    public class Company : BaseEntity
    {
        public Company()
        {
            Users = new List<User>();
        }
        public string CompanyName { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
