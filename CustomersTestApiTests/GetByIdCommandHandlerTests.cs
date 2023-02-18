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
  public class GetByIdCommandHandlerTests : TestsBase
  {
    /// <summary>
    /// System under Test
    /// </summary>
    private readonly GetByIdCommandHandler _sut;

    /// <summary>
    /// The customer repository mock
    /// </summary>
    private readonly Mock<ICustomersRepository> _customerRepositoryMock = new Mock<ICustomersRepository>();

    /// <summary>
    /// The mapper mock
    /// </summary>
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

    /// <summary>
    /// Initializes a new instance of the <see cref="GetByIdCommandHandlerTests"/> class.
    /// </summary>
    public GetByIdCommandHandlerTests()
    {
      _sut = new GetByIdCommandHandler(_mapperMock.Object, _customerRepositoryMock.Object);
    }

    /// <summary>
    /// Handles the customers return customers.
    /// </summary>
    [Fact]
    public async Task Handle_CustomerExists_ReturnCustomer()
    {
      //Arrange
      var command = new GetByIdCommand { UserId = userId, Id = 1 };

      var customerDto = GetCustomerDto();

      var customer = GetCustomer();

      _mapperMock.Setup(o => o.Map<CustomerDto>(customer)).Returns(customerDto);
     
      _customerRepositoryMock.Setup(o => o.GetById(command.Id, command.UserId)).ReturnsAsync(customer);

      var commandResultExpected = CommandResult.Success(data: customerDto);

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.Data, commandResult.Data);

    }

    [Fact]
    public async Task Handle_CustomersNotExists_ReturnNoData()
    {
      //Arrange
      var command = new GetByIdCommand { UserId = userId, Id = 1 };
      var customerDto = GetCustomerDto();
      Customer customer = null;

      _mapperMock.Setup(o => o.Map<CustomerDto>(customer)).Returns(customerDto);

      _customerRepositoryMock.Setup(o => o.GetById(command.Id, command.UserId)).ReturnsAsync(customer);

      var commandResultExpected = CommandResult.SuccessWithNoData();

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.Data, commandResult.Data);

    }

    /// <summary>
    /// Handles the exception returns error.
    /// </summary>
    [Fact]
    public async Task Handle_Exception_ReturnsError()
    {
      //Arrange
      var commandResultExpected = CommandResult.Fail("Object reference not set to an instance of an object.", Constants.LOGIC_EXCEPTION_CODE, "");

      GetByIdCommand command = null;

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
    }
  }
}
