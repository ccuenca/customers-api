using System.ComponentModel.DataAnnotations;

namespace CustomersTestApi.Domain.Commands
{
  /// <summary>
  /// 
  /// </summary>
  public class BaseCommand
  {
    /// <summary>
    /// Gets or sets the name of the current user.
    /// </summary>
    /// <value>
    /// The name of the current user.
    /// </value>
    [Required(ErrorMessage = "UserId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than zero")]
    public int UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ParametroPrueba1 { get; set; }

  }
}
