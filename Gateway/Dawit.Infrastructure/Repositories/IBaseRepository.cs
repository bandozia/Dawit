﻿using Dawit.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawit.Infrastructure.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        public Task<T> InsertAsync(T item);
        public Task<IEnumerable<T>> GetAllAsync();        
        public Task<T> GetByIdAsync(Guid id);
    }
}
