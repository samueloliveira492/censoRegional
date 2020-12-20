using MediatR;

namespace CensoRegional.Domain.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
