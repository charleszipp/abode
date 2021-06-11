using System.Threading;
using System.Threading.Tasks;

namespace Abode.Core
{
    public interface IHandleCommand<in TCommand>
        where TCommand : ICommand
    {
        Task Execute(TCommand command, CancellationToken cancellationToken);
    }
}
