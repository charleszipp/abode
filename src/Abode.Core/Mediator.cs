using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abode.Core
{
    public class Mediator
    {
        private readonly IServiceProvider _resolver;

        public Mediator(IServiceProvider resolver) => _resolver = resolver;

        public Task ExecuteAsync(ICommand command, CancellationToken cancellationToken)
        {
            var invoker = (VoidCommandInvoker)Activator.CreateInstance(typeof(VoidCommandInvoker<>).MakeGenericType(command.GetType()));
            return invoker.InvokeAsync(command, _resolver, cancellationToken);
        }

        public Task<TResult> Query<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        {
            var invoker = (QueryInvoker<TResult>)Activator.CreateInstance(typeof(QueryInvoker<,>).MakeGenericType(query.GetType(), typeof(TResult)));
            return invoker.InvokeAsync(query, _resolver, cancellationToken);
        }
    }
}
