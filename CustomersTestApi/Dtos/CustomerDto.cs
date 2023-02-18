using System;
using System.ComponentModel.DataAnnotations;

namespace CustomersTestApi.Dtos
{
  /// <summary>
  /// 
  /// </summary>
  [Serializable]
  public class CustomerDto
  {
    /// <summary>
    /// Gets or sets the unique identifier, for new customers send zero.
    /// </summary>
    /// <value>
    /// The unique identifier.
    /// </value>
    [Required(ErrorMessage = "UniqueId is required if new send zero")]
    public int UniqueId { get; set; }

    /// <summary>
    /// Gets or sets the names.
    /// </summary>
    /// <value>
    /// The names.
    /// </value>
    [Required(ErrorMessage = "Names is required")]
    public string Names { get; set; }

    /// <summary>
    /// Gets or sets the sure names.
    /// </summary>
    /// <value>
    /// The sure names.
    /// </value>
    [Required(ErrorMessage = "SureNames is required")]
    public string SureNames { get; set; }

    /// <summary>
    /// Gets or sets the birth date.
    /// </summary>
    /// <value>
    /// The birth date.
    /// </value>
    [Required(ErrorMessage = "BirthDate is required")]
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>
    /// The address.
    /// </value>
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    /// <value>
    /// The email address.
    /// </value>
    [Required(ErrorMessage = "EmailAddress is required")]
    public string EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the identification number.
    /// </summary>
    /// <value>
    /// The identification number.
    /// </value>
    [Required(ErrorMessage = "IdentificationNumber is required")]
    public string IdentificationNumber { get; set; }

    /// <summary>
    /// Gets the phone number.
    /// </summary>
    /// <value>
    /// The phone number.
    /// </value>
    [Required(ErrorMessage = "PhoneNumber is required")]
    public string PhoneNumber { get; set; }
  }
}
