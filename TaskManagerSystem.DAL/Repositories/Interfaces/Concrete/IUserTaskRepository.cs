using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.DAL.Repositories.Interfaces.Concrete
{
    public interface IUserTaskRepository
    {
        void Create(UserTask entity);
        void Delete(UserTask entity);
        List<UserTask> GetDefaults(System.Linq.Expressions.Expression<Func<UserTask, bool>> expression);
        UserTask GetDefault(System.Linq.Expressions.Expression<Func<UserTask, bool>> expression);
    }
}
