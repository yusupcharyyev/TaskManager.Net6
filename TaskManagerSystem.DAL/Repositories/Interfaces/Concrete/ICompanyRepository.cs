using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.DAL.Repositories.Interfaces.Abstract;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.DAL.Repositories.Interfaces.Concrete
{
    public interface ICompanyRepository: IBaseRepository<Company>
    {
    }
}
