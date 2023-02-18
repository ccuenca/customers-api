using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CustomersTestApi.Domain.Commands
{
  /// <summary>
  /// 
  /// </summary>
  public class DeleteCommand : BaseCommand, IRequest<CommandResult>
  {

    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    /// <value>
    /// The customer identifier.
    /// </value>
    [Required(ErrorMessage = "CustomerId is required")]
    public int CustomerId { get; set; }
  }
}
