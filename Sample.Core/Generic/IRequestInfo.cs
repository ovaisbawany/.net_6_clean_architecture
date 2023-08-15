using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Generic
{
    public interface IRequestInfo
    {
        long UserId { get; }

        string UserName { get; }

        string Role { get; }
    }
}
