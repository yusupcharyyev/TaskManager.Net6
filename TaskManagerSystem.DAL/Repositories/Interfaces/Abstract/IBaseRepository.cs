using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Abstract;

namespace TaskManagerSystem.DAL.Repositories.Interfaces.Abstract
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Any(Expression<Func<T, bool>> expression);
        T GetDefault(Expression<Func<T, bool>> expression); 
        List<T> GetDefaults(Expression<Func<T, bool>> expression);
    }
}
