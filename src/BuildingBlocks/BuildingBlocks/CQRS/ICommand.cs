using MediatR;

namespace BuildingBlocks.CQRS;
// no response
public interface ICommand : ICommand<Unit>
{
}
// return response
public interface ICommand<out Tresponse> : IRequest<Tresponse>
{
}
