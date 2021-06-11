using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Abode.Core
{
    internal abstract class QueryInvoker<TResult>
    {
        internal abstract Task<TResult> InvokeAsync(IQuery<TResult> query, IServiceProvider resolver, CancellationToken cancellationToken);
    }

    internal class QueryInvoker<TQuery, TResult> : QueryInvoker<TResult>
        where TQuery : IQuery<TResult>
    {
        internal override Task<TResult> InvokeAsync(IQuery<TResult> query, IServiceProvider resolver, CancellationToken cancellationToken) =>
            resolver.GetService<IHandleQuery<TQuery, TResult>>().Execute((TQuery)query, cancellationToken);
    }
}
