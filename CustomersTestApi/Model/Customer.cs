using System;

namespace CustomersTestApi.Model
{
  /// <summary>
  /// 
  /// </summary>
  public class Customer
  {
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    /// <value>
    /// The unique identifier.
    /// </value>
    public int UniqueId { get; set; }

    /// <summary>
    /// Gets or sets the names.
    /// </summary>
    /// <value>
    /// The names.
    /// </value>
    public string Names { get; set; }

    /// <summary>
    /// Gets or sets the sure names.
    /// </summary>
    /// <value>
    /// The sure names.
    /// </value>
    public string SureNames { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    /// <value>
    /// The created date.
    /// </value>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the birth date.
    /// </summary>
    /// <value>
    /// The birth date.
    /// </value>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Gets the phone number.
    /// </summary>
    /// <value>
    /// The phone number.
    /// </value>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>
    /// The address.
    /// </value>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    /// <value>
    /// The email address.
    /// </value>
    public string EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the identification number.
    /// </summary>
    /// <value>
    /// The identification number.
    /// </value>
    public string IdentificationNumber { get; set; }

   

    
  }
}
