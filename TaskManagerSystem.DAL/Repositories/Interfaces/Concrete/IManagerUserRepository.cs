using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.DAL.Repositories.Interfaces.Concrete
{
    public interface IManagerUserRepository
    {
        void Create(ManagerUser entity);
        void Delete(ManagerUser entity);
        List<ManagerUser> GetDefaults(System.Linq.Expressions.Expression<Func<ManagerUser, bool>> expression);
        ManagerUser GetDefault(System.Linq.Expressions.Expression<Func<ManagerUser, bool>> expression);
    }
}
