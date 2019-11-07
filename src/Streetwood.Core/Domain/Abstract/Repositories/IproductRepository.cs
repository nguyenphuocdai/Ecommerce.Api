﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Streetwood.Core.Domain.Entities;

namespace Streetwood.Core.Domain.Abstract.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetAndEnsureExistAsync(int id);

        Task<IList<Product>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
