using MediatR;

namespace CustomersTestApi.Domain.Commands
{
  /// <summary>
  /// 
  /// </summary>
  /// <seealso cref="CustomersTestApi.Domain.Commands.BaseCommand" />
  public class GetAllCommand : BaseCommand, IRequest<CommandResult>
  {

  }
}
