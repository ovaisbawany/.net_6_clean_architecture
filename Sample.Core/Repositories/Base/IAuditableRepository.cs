using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Repositories.Base
{

    public interface IAuditableRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    {
        Task HardDelete(TEntity entity);
        Task HardDelete(IList<TEntity> entities);

    }
}
