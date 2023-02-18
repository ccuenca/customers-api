using CustomersTestApi.Dtos;
using CustomersTestApi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomersTestApiUnitTest
{
  /// <summary>
  /// 
  /// </summary>
  public abstract class TestsBase
  {

    /// <summary>
    /// The user identifier
    /// </summary>
    protected int userId = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestsBase"/> class.
    /// </summary>
    public TestsBase()
    {

    }

    /// <summary>
    /// Gets the customer.
    /// </summary>
    /// <returns></returns>
    protected static Customer GetCustomer()
    {
      var customer = new Customer
      {
        Names = "Cristhian",
        SureNames = "Cuenca",
        UniqueId = 0,
        Address = "Ciudad Country - Alondra - Casa 3",
        PhoneNumber = "3013381581",
        IdentificationNumber = "CC16917723",
        EmailAddress = "ccuenca24@gmail.com",
        BirthDate = new DateTime(1981, 05, 24)
      };

      return customer;
    }

    /// <summary>
    /// Gets the customer dto.
    /// </summary>
    /// <returns></returns>
    protected static CustomerDto GetCustomerDto()
    {
      var customerDto = new CustomerDto
      {
        Names = "Cristhian",
        SureNames = "Cuenca",
        UniqueId = 0,
        Address = "Ciudad Country - Alondra - Casa 3",
        PhoneNumber = "3013381581",
        IdentificationNumber = "CC16917723",
        EmailAddress = "ccuenca24@gmail.com",
        BirthDate = new DateTime(1981, 05, 24)
      };
      return customerDto;
    }

  }
}
