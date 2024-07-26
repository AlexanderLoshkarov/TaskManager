using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaskManager.Console.EFCore;

[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]

namespace MyNamespace;

public class Startup : FunctionsStartup
{
  public override void Configure(IFunctionsHostBuilder builder)
  {
    builder.Services.AddDbContext<TaskManagerDbContext>();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.Load("TaskManager.Mediatr")));
  }
}