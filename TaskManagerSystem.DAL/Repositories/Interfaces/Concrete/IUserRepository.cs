using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.DAL.Repositories.Interfaces.Concrete
{
    public interface IUserRepository
    {
        void Create(User entity);
        void Update(User entity);
        void Delete(User entity);
        bool Any(Expression<Func<User, bool>> expression);
        User GetDefault(Expression<Func<User, bool>> expression);
        List<User> GetDefaults(Expression<Func<User, bool>> expression);
    }
}
