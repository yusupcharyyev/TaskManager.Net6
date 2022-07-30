using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.DAL.Context;
using TaskManagerSystem.DAL.Repositories.Interfaces.Concrete;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.DAL.Repositories.Concrete
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly ProjectContext _projectContext;
        private readonly DbSet<UserTask> _table;

        public UserTaskRepository(ProjectContext projectContext)
        {
            _projectContext = projectContext;
            _table = projectContext.Set<UserTask>();
        }
        public void Create(UserTask entity)
        {
            _table.Add(entity);
            _projectContext.SaveChanges();
        }

        public void Delete(UserTask entity)
        {
            _table.Remove(entity);
            _projectContext.SaveChanges();
        }

        public UserTask GetDefault(Expression<Func<UserTask, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        public List<UserTask> GetDefaults(Expression<Func<UserTask, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }
    }
}
