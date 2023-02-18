using AutoMapper;
using CustomersTestApi.Domain.Commands;
using CustomersTestApi.Domain.Handlers;
using CustomersTestApi.Dtos;
using CustomersTestApi.Model;
using CustomersTestApi.Persistance.Repositories;
using CustomersTestApi.Support;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomersTestApiUnitTest
{
  public class GetAllCommandHandlerTests : TestsBase
  {

    /// <summary>
    /// System under Test
    /// </summary>
    private readonly GetAllCommandHandler _sut;

    /// <summary>
    /// The customer repository mock
    /// </summary>
    private readonly Mock<ICustomersRepository> _customerRepositoryMock = new Mock<ICustomersRepository>();

    /// <summary>
    /// The mapper mock
    /// </summary>
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

    /// <summary>
    /// The general conf mock
    /// </summary>
    private readonly Mock<IOptions<GeneralConf>> _generalConfMock = new Mock<IOptions<GeneralConf>>();

    /// <summary>
    /// The conf
    /// </summary>
    private readonly GeneralConf _conf;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCommandHandlerTests"/> class.
    /// </summary>
    public GetAllCommandHandlerTests()
    {
      _conf = new GeneralConf();
      _generalConfMock.Setup(o => o.Value).Returns(_conf);
      _sut = new GetAllCommandHandler(_mapperMock.Object, _customerRepositoryMock.Object, _generalConfMock.Object);
    }

    /// <summary>
    /// Handles the customers return customers.
    /// </summary>
    [Fact]
    public async void Handle_Customers_ReturnCustomers()
    {
      //Arrange

      var command = new GetAllCommand {  UserId = userId };

      var customersDto = new List<CustomerDto>
      {
        GetCustomerDto(),
        GetCustomerDto(),
        GetCustomerDto()
      };

      var customers = new List<Customer>
      {
        GetCustomer(),
        GetCustomer(),
        GetCustomer()
      };

      _mapperMock.Setup(o => o.Map<List<CustomerDto>>(customers)).Returns(customersDto);
      _customerRepositoryMock.Setup(o => o.GetAll(userId)).ReturnsAsync(customers);

      var list = new Collection
      {
        Count = customersDto.Count,
        List = customersDto
      };

      var commandResultExpected = CommandResult.Success(data: list);

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(((Collection)commandResultExpected.Data).List, ((Collection)commandResult.Data).List);

    }

    /// <summary>
    /// Handles the exception returns error.
    /// </summary>
    [Fact]
    public async Task Handle_Exception_ReturnsError()
    {
      //Arrange
      var commandResultExpected = CommandResult.Fail("Object reference not set to an instance of an object.", Constants.LOGIC_EXCEPTION_CODE, "");

      GetAllCommand command = null;

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
    }
  }
}
