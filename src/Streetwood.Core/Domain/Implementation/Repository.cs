﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Streetwood.Core.Domain.Abstract;
using Streetwood.Core.Exceptions;

namespace Streetwood.Core.Domain.Implementation
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public Repository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public async Task<IList<T>> GetListAsync()
            => await dbSet.ToListAsync();

        public IQueryable<T> GetQueryable()
            => dbSet.AsQueryable()
                .AsNoTracking();

        public async Task<T> GetAsync(Guid id)
            => await dbSet.FindAsync(id);

        public async Task<T> GetAndEnsureExistAsync(Guid id)
        {
            var result = await dbSet.FindAsync(id);
            if (result == null)
            {
                throw new StreetwoodException(ErrorCode.GenericNotExist(typeof(T)));
            }

            return result;
        }

        public async Task UpdateAsync(T entity)
            => await Task.FromResult(dbSet.Update(entity));

        public async Task DeleteAsync(T entity)
            => await Task.FromResult(dbSet.Remove(entity));

        public async Task SaveChangesAsync()
        {
            try
            {
                if (await dbContext.SaveChangesAsync() < 0)
                {
                    throw new StreetwoodException(ErrorCode.CannotSaveDatabase);
                }
            }
            catch (Exception ex)
            {
                throw new StreetwoodException(ErrorCode.CannotSaveDatabase, ex.Message, ex);
            }
        }
    }
}
