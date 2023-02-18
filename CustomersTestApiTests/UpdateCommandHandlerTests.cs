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
using System.Xml;
using Xunit;

namespace CustomersTestApiUnitTest
{
  public class UpdateCommandHandlerTests : TestsBase
  {
    /// <summary>
    /// System under Test
    /// </summary>
    private readonly UpdateCommandHandler _sut;

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
    public UpdateCommandHandlerTests()
    {
      _conf = new GeneralConf();
      _generalConfMock.Setup(o => o.Value).Returns(_conf);
      _sut = new UpdateCommandHandler(_mapperMock.Object, _customerRepositoryMock.Object, _generalConfMock.Object);
    }

    /// <summary>
    /// Handles the customer exists returns error.
    /// </summary>    
    [Fact]
    public async Task Handle_CustomerNotExists_ReturnsError()
    {
      //Arrange
      var customerDto = GetCustomerDto();
      customerDto.UniqueId = 1;
      var command = new UpdateCommand { Customer = customerDto, UserId = userId };

      Customer oldCustomer = null;

      _customerRepositoryMock.Setup(o => o.GetById(customerDto.UniqueId, userId)).ReturnsAsync(oldCustomer);

      var commandResultExpected = CommandResult.Fail(Messages.OBJECT_NOT_FOUND_MESSAGE,
          Constants.LOGIC_EXCEPTION_CODE, $"CustomerId: {command.Customer.UniqueId}");

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonCode, commandResult.FailureReasonCode);
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
      Assert.Equal(commandResultExpected.FailureReasonExcepcion, commandResult.FailureReasonExcepcion);
    }

    /// <summary>
    /// Handles the customer exists not save return error.
    /// </summary>
    [Fact]
    public async Task Handle_CustomerExistsNotSave_ReturnError()
    {
      //Arrange
      var customerDto = GetCustomerDto();
      customerDto.UniqueId = 1;
      var command = new UpdateCommand { Customer = customerDto, UserId = userId };

      Customer oldCustomer = GetCustomer();
      Customer customerToUpdate = GetCustomer();

      _customerRepositoryMock.Setup(o => o.GetById(customerDto.UniqueId, userId)).ReturnsAsync(oldCustomer);

      _mapperMock.Setup(o => o.Map<Customer>(customerDto)).Returns(customerToUpdate);

      _customerRepositoryMock.Setup(o => o.Update(customerToUpdate, userId)).ReturnsAsync(0);

      var commandResultExpected = CommandResult.Fail(Messages.OperationNotSuccessfulMessage, Constants.LOGIC_EXCEPTION_CODE, string.Empty);

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonCode, commandResult.FailureReasonCode);
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
      Assert.Equal(commandResultExpected.FailureReasonExcepcion, commandResult.FailureReasonExcepcion);

    }

    /// <summary>
    /// Gets or sets the handle customer exists and save not load data returns error.
    /// </summary>
    /// <value>
    /// The handle customer exists and save not load data returns error.
    /// </value>
    [Fact]
    public async Task Handle_CustomerExistsAndSaveNotLoadData_ReturnsError()
    {
      var customerDto = GetCustomerDto();
      customerDto.UniqueId = 1;
      var command = new UpdateCommand { Customer = customerDto, UserId = userId };

      Customer oldCustomer = GetCustomer();
      Customer customerToUpdate = GetCustomer();
      customerToUpdate.UniqueId = customerDto.UniqueId;
      Customer updatedCustomer = null;

      var queueResults = new Queue<Customer>();
      queueResults.Enqueue(oldCustomer);
      queueResults.Enqueue(updatedCustomer);

      _mapperMock.Setup(o => o.Map<Customer>(customerDto)).Returns(customerToUpdate);
      
      _customerRepositoryMock.Setup(o => o.GetById(customerDto.UniqueId, userId)).ReturnsAsync(queueResults.Dequeue);
      _customerRepositoryMock.Setup(o => o.Update(customerToUpdate, userId)).ReturnsAsync(1);
      _customerRepositoryMock.Setup(o => o.GetById(customerToUpdate.UniqueId, userId)).ReturnsAsync(queueResults.Dequeue);

      var commandResultExpected = CommandResult.Fail(Messages.OBJECT_NOT_FOUND_MESSAGE, Constants.LOGIC_EXCEPTION_CODE, $"CustomerId: {customerToUpdate.UniqueId}");


      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonCode, commandResult.FailureReasonCode);
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
      Assert.Equal(commandResultExpected.FailureReasonExcepcion, commandResult.FailureReasonExcepcion);

    }

    /// <summary>
    /// Handles the customer exists and save returns updated customer.
    /// </summary>
    [Fact]
    public async Task Handle_CustomerExistsAndSave_ReturnsUpdatedCustomer()
    {
      var customerDto = GetCustomerDto();
      customerDto.UniqueId = 1;
      var command = new UpdateCommand { Customer = customerDto, UserId = userId };

      Customer oldCustomer = GetCustomer();
      Customer customerToUpdate = GetCustomer();
      customerToUpdate.UniqueId = customerDto.UniqueId;
      Customer updatedCustomer = GetCustomer();
      updatedCustomer.UniqueId = customerDto.UniqueId;

      _mapperMock.Setup(o => o.Map<Customer>(customerDto)).Returns(customerToUpdate);
      _mapperMock.Setup(o => o.Map<CustomerDto>(updatedCustomer)).Returns(customerDto);

      _customerRepositoryMock.Setup(o => o.GetById(customerDto.UniqueId, userId)).ReturnsAsync(oldCustomer);
      _customerRepositoryMock.Setup(o => o.Update(customerToUpdate, userId)).ReturnsAsync(1);
      _customerRepositoryMock.Setup(o => o.GetById(customerToUpdate.UniqueId, userId)).ReturnsAsync(updatedCustomer);

      var commandResultExpected = CommandResult.Success(data: customerDto, id: customerDto.UniqueId);

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
      var commandResultExpected = CommandResult.Fail("Object reference not set to an instance of an object.", Constants.LOGIC_EXCEPTION_CODE, "");

      UpdateCommand command = null;

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
    }

  }
}
