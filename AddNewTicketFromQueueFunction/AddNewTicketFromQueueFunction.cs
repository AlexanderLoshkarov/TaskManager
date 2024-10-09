using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaskManager.Mediatr;

namespace AddNewTicketFromQueueFunction
{
  public class AddNewTicketFromQueueFunction
  {
    private readonly IMediator _mediator;

    public AddNewTicketFromQueueFunction(IMediator mediator)
    {
      _mediator = mediator;
    }

    [FunctionName(nameof(AddNewTicketFromQueueFunction))]
    public async Task Run([ServiceBusTrigger("queue", Connection = "ServiceBusConnection")] string addNewTicketQueueItem, ILogger log)
    {
      log.LogInformation($"C# ServiceBus queue trigger function processed message: {addNewTicketQueueItem}");

      AddNewTicketRequest newTicketRequest = JsonConvert.DeserializeObject<AddNewTicketRequest>(addNewTicketQueueItem);

      Result<Guid> result = await _mediator.Send(newTicketRequest);
    }
  }
}
