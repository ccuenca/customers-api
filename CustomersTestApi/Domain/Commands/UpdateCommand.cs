using CustomersTestApi.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CustomersTestApi.Domain.Commands
{
  /// <summary>
  /// 
  /// </summary>
  /// <seealso cref="CustomersTestApi.Domain.Commands.BaseCommand" />
  public class UpdateCommand : BaseCommand, IRequest<CommandResult>
  {
    
    /// <summary>
    /// Gets or sets the customer.
    /// </summary>
    /// <value>
    /// The customer.
    /// </value>
    [Required(ErrorMessage = "Customer is required")]
    public CustomerDto Customer { get; set; }
  }
}
