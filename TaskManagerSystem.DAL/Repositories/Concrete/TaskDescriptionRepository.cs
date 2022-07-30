using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.DAL.Context;
using TaskManagerSystem.DAL.Repositories.Abstract;
using TaskManagerSystem.DAL.Repositories.Interfaces.Concrete;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.DAL.Repositories.Concrete
{
    public class TaskDescriptionRepository : BaseRepository<TaskDescription>, ITaskDescriptionRespository
    {
        public TaskDescriptionRepository(ProjectContext context) : base(context)
        {
        }
    }
}
