using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MU.Common.Services
{
    public interface IDataService<in TEntity>
         where TEntity : class
    {
        Task MarkMessageAsPublished(int id);

        Task Save(TEntity entity);
    }
}
