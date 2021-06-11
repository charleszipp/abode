using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Abode.Core
{
    internal abstract class VoidCommandInvoker
    {
        internal abstract Task InvokeAsync(ICommand command, IServiceProvider resolver, CancellationToken cancellationToken);
    }

    internal class VoidCommandInvoker<TCommand> : VoidCommandInvoker
        where TCommand : ICommand
    {
        internal override Task InvokeAsync(ICommand command, IServiceProvider resolver, CancellationToken cancellationToken) =>
            resolver.GetService<IHandleCommand<TCommand>>().Execute((TCommand)command, cancellationToken);
    }
}
