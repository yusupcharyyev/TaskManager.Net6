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
using TaskManagerSystem.Models.Enums;

namespace TaskManagerSystem.DAL.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectContext _projectContext;
        private readonly DbSet<User> _table;

        public UserRepository(ProjectContext projectContext)
        {
            _projectContext = projectContext;
            _table = projectContext.Set<User>();
        }
        public bool Any(Expression<Func<User, bool>> expression)
        {
            return _table.Any(expression);
        }

        public void Create(User entity)
        {
            _table.Add(entity);
            _projectContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            entity.Statu = Statu.Passive;
            _projectContext.SaveChanges();
        }

        public User GetDefault(Expression<Func<User, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        public List<User> GetDefaults(Expression<Func<User, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }

        public void Update(User entity)
        {
            entity.Statu = Statu.Modified;
            _table.Update(entity);
            _projectContext.SaveChanges();
        }
    }
}
