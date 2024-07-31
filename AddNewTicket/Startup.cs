using FluentValidation;
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
    var assembly = AppDomain.CurrentDomain.Load("TaskManager.Mediatr");

    builder.Services.AddDbContext<TaskManagerDbContext>();
    builder.Services.AddValidatorsFromAssembly(assembly);
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
  }
}