using MediatR;

namespace TaskManager.Mediatr
{
  public record AddNewTicketRequest(string Title, string? Description) : IRequest<Result<Guid>>;
}
