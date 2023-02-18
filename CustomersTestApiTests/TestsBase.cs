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
      var customer = new Customer();
      customer.Names = "Cristhian";
      customer.SureNames = "Cuenca";
      customer.UniqueId = 0;
      customer.Address = "Ciudad Country - Alondra - Casa 3";
      customer.PhoneNumber = "3013381581";
      customer.IdentificationNumber = "CC16917723";
      customer.EmailAddress = "ccuenca24@gmail.com";
      customer.BirthDate = new DateTime(1981, 05, 24);
      return customer;
    }

    /// <summary>
    /// Gets the customer dto.
    /// </summary>
    /// <returns></returns>
    protected static CustomerDto GetCustomerDto()
    {
      var customerDto = new CustomerDto();
      customerDto.Names = "Cristhian";
      customerDto.SureNames = "Cuenca";
      customerDto.UniqueId = 0;
      customerDto.Address = "Ciudad Country - Alondra - Casa 3";
      customerDto.PhoneNumber = "3013381581";
      customerDto.IdentificationNumber = "CC16917723";
      customerDto.EmailAddress = "ccuenca24@gmail.com";
      customerDto.BirthDate = new DateTime(1981, 05, 24);
      return customerDto;
    }

  }
}
