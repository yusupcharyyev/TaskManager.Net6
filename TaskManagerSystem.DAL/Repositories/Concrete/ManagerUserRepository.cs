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
    public class ManagerUserRepository : IManagerUserRepository
    {
        private readonly ProjectContext _projectContext;
        private readonly DbSet<ManagerUser> _table;

        public ManagerUserRepository(ProjectContext projectContext)
        {
            _projectContext = projectContext;
            _table = projectContext.Set<ManagerUser>();
        }
        public void Create(ManagerUser entity)
        {
            _table.Add(entity);
            _projectContext.SaveChanges();
        }

        public void Delete(ManagerUser entity)
        {
            _table.Remove(entity);
            _projectContext.SaveChanges();
        }

        public ManagerUser GetDefault(Expression<Func<ManagerUser, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        public List<ManagerUser> GetDefaults(Expression<Func<ManagerUser, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }
    }
}
