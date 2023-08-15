using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Entities.Base
{
    public interface IAuditModel<TKey> : IAuditModel, IBase<TKey>
    {
    }

    public interface IAuditModel : IBase
    {
        DateTime CreatedOn { get; set; }

        DateTime? LastModifiedOn { get; set; }

        bool IsDeleted { get; set; }

        long CreatedBy { get; set; }

        long? LastModifiedBy { get; set; }
    }
}
