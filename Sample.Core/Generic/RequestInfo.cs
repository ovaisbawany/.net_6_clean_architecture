using Microsoft.Extensions.DependencyInjection;

namespace Sample.Core.Generic
{
    public class RequestInfo : IRequestInfo
    {
        private readonly IServiceScope Scope;

        public RequestInfo(IServiceProvider serviceProvider)
        {
            Scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }

        public string Role => "test";

        public string UserName => "asd";

        public long UserId => 1;
    }
}
