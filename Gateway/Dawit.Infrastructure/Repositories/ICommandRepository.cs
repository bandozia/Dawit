using Dawit.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories
{
    public interface ICommandRepository<T> where T : BaseModel
    {
        Task<T> InsertAsync(T item);
    }
}
