using MediatR;
using TaskManager.Console.EFCore;
using FluentValidation; 

namespace TaskManager.Mediatr
{
  public class Validator : AbstractValidator<AddNewTicketRequest>
  {
    public Validator()
    {
      RuleFor(x => x.Title).NotNull().NotEmpty();
    }
  }
  public sealed class AddNewTicketRequestHandler : IRequestHandler<AddNewTicketRequest, Result<Guid>>
  {
    private readonly TaskManagerDbContext _dbContext;
    private readonly IValidator<AddNewTicketRequest> _validator;

    public AddNewTicketRequestHandler(
      TaskManagerDbContext dbContext, 
      IValidator<AddNewTicketRequest> validator)
    {
      _dbContext = dbContext;
      _validator = validator;
    }

    public async Task<Result<Guid>> Handle(AddNewTicketRequest request, CancellationToken cancellationToken)
    {
      var validationResult = _validator.Validate(request);
      if (!validationResult.IsValid)
      {
        return Result<Guid>.Failure(new Error(
          "AddNewTicket.Validation",
          validationResult.ToString()
          ));
      }

      var newId = Guid.NewGuid();
      var ticket = new Ticket
      {
        Id = newId.ToString(),
        Title = request.Title,
        Description = request.Description,
        TicketStatus = TicketStatus.New,
        CreatedAt = DateTime.UtcNow
      };

      try 
      {
        _dbContext.Tickets.Add(ticket);
        await _dbContext.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        return Result<Guid>.Failure(new Error(
          "AddNewTicket.Save",
          "Failed to save new Task"
          ));
      }

      return Result<Guid>.Success(newId);
    }
  }
}
