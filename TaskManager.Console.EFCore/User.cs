using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Console.EFCore
{
  public class User
  {
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required UserRole UserRole { get; set; }
  }
}
