using System.Threading;
using System.Threading.Tasks;

namespace Abode.Core
{
    public interface IHandleQuery<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Execute(TQuery query, CancellationToken cancellationToken);
    }
}
