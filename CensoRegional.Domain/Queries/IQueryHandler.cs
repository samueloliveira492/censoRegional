using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CensoRegional.Domain.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}
