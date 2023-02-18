using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CustomersTestApi.Domain.Commands
{
  /// <summary>
  /// 
  /// </summary>
  /// <seealso cref="CustomersTestApi.Domain.Commands.BaseCommand" />
  public class GetByIdCommand : BaseCommand, IRequest<CommandResult>
  {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
  }
}
