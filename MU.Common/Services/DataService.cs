using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MU.Common.Services
{
    public abstract class DataService<TEntity> : IDataService<TEntity>
         where TEntity : class
    {
        protected DataService(DbContext db) => this.Data = db;

        protected DbContext Data { get; }

        protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task Save(TEntity entity)
        {
            this.Data.Update(entity);

            await this.Data.SaveChangesAsync();
        }

        public Task MarkMessageAsPublished(int id)
        {
            throw new NotImplementedException();
        }
    }
}
