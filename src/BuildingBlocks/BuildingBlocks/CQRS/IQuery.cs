using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
    // Generic Filter That the response not null 
    where TResponse : notnull
{
}