using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using TaskManager.Mediatr;

namespace AddNewTicket
{
  public class AddNewTicketFunction
  {
    private readonly IMediator _mediator;

    public AddNewTicketFunction(IMediator mediator)
    {
      _mediator = mediator;
    }

    [FunctionName(nameof(AddNewTicketFunction))]
    public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
    {
      log.LogInformation("Adding New Ticket");

      Guid id = await _mediator.Send(new AddNewTicketRequest("New Ticket Test1", "Description of Test ticket"));

      return new CreatedResult("/ticket/", id);
    }
  }
}
