using AutoMapper;
using CustomersTestApi.Domain.Commands;
using CustomersTestApi.Domain.Handlers;
using CustomersTestApi.Dtos;
using CustomersTestApi.Model;
using CustomersTestApi.Persistance.Repositories;
using CustomersTestApi.Support;
using CustomersTestApiUnitTest;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace CustomersTestApiUnitTest
{
  /// <summary>
  /// 
  /// </summary>
  public class CreateCommandHandlerTests : TestsBase
  {
    /// <summary>
    /// System under Test
    /// </summary>
    private readonly CreateCommandHandler _sut;

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
    public CreateCommandHandlerTests()
    {
      _conf = new GeneralConf();
      _generalConfMock.Setup(o => o.Value).Returns(_conf);
      _sut = new CreateCommandHandler(_mapperMock.Object, _customerRepositoryMock.Object, _generalConfMock.Object);
    }

    /// <summary>
    /// Handles the customer exists returns error.
    /// </summary>    
    [Fact]
    public async Task Handle_CustomerExists_ReturnsError()
    {
      //Arrange
      var customerDto = GetCustomerDto();
      var command = new CreateCommand { Customer = customerDto, UserId = userId };

      var existingCustomer = new Customer();

      _customerRepositoryMock.Setup(o => o.GetByIdentificacion(customerDto.IdentificationNumber, userId)).ReturnsAsync(existingCustomer);

      var commandResultExpected = CommandResult.Fail(Messages.ObjectExists, Constants.LOGIC_EXCEPTION_CODE,
            $"CustomerIdentification: {command.Customer.IdentificationNumber}");

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonCode, commandResult.FailureReasonCode);
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
      Assert.Equal(commandResultExpected.FailureReasonExcepcion, commandResult.FailureReasonExcepcion);
    }

    /// <summary>
    /// Handles the customer not exists returns error.
    /// </summary>
    [Fact]
    public async Task Handle_CustomerNotExists_ReturnsError()
    {
      //Arrange
      CustomerDto customerDto = GetCustomerDto();

      var existingCustomer = new Customer();
      existingCustomer = null;

      Customer customer = GetCustomer();

      var commandResultExpected = CommandResult.Fail(Messages.OperationNotSuccessfulMessage, Constants.LOGIC_EXCEPTION_CODE, string.Empty);

      _customerRepositoryMock.Setup(o => o.GetByIdentificacion(customerDto.IdentificationNumber, userId)).ReturnsAsync(existingCustomer);
      _customerRepositoryMock.Setup(o => o.Create(customer, userId)).ReturnsAsync(0);

      var command = new CreateCommand { Customer = customerDto, UserId = userId };

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonCode, commandResult.FailureReasonCode);
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
      Assert.Equal(commandResultExpected.FailureReasonExcepcion, commandResult.FailureReasonExcepcion);
    }

    /// <summary>
    /// Handles the customer not exists and not save1 returns error.
    /// </summary>
    [Fact]
    public async Task Handle_CustomerNotExistsAndNotSave_ReturnsError()
    {
      //Arrange
      int newCustomerId = 100;

      CustomerDto customerDto = GetCustomerDto();
      Customer customerToCreate = GetCustomer();
      customerToCreate.UniqueId = 0;

      Customer createdCustomer = GetCustomer();
      createdCustomer = null;

      var existingCustomer = new Customer();
      existingCustomer = null;

      var commandResultExpected = CommandResult.Fail(Messages.OBJECT_NOT_FOUND_MESSAGE, Constants.LOGIC_EXCEPTION_CODE, $"CustomerId: {newCustomerId}");

      _mapperMock.Setup(o => o.Map<Customer>(customerDto)).Returns(customerToCreate);

      _customerRepositoryMock.Setup(o => o.GetByIdentificacion(customerDto.IdentificationNumber, userId)).ReturnsAsync(existingCustomer);
      _customerRepositoryMock.Setup(o => o.GetById(newCustomerId, userId)).ReturnsAsync(createdCustomer);
      _customerRepositoryMock.Setup(o => o.Create(customerToCreate, userId)).ReturnsAsync(newCustomerId);

      var command = new CreateCommand { Customer = customerDto, UserId = userId };

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert

      Assert.Equal(commandResultExpected.FailureReasonCode, commandResult.FailureReasonCode);
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
      Assert.Equal(commandResultExpected.FailureReasonExcepcion, commandResult.FailureReasonExcepcion);
    }

    /// <summary>
    /// Handles the customer not exists and save returns created customer.
    /// </summary>
    [Fact]
    public async Task Handle_CustomerNotExistsAndSave_ReturnsCreatedCustomer()
    {
      //Arrange
      int newCustomerId = 100;

      CustomerDto customerDto = GetCustomerDto();
      Customer customerToCreate = GetCustomer();
      CustomerDto customerCreatedDto = GetCustomerDto();
      customerCreatedDto.UniqueId = newCustomerId;

      customerToCreate.UniqueId = 0;

      Customer createdCustomer = GetCustomer();
      createdCustomer.UniqueId = newCustomerId;

      var existingCustomer = new Customer();
      existingCustomer = null;

      var commandResultExpected = CommandResult.Success(data: customerCreatedDto, id: newCustomerId);

      _mapperMock.Setup(o => o.Map<Customer>(customerDto)).Returns(customerToCreate);
      _mapperMock.Setup(o => o.Map<CustomerDto>(createdCustomer)).Returns(customerCreatedDto);

      _customerRepositoryMock.Setup(o => o.GetByIdentificacion(customerDto.IdentificationNumber, userId)).ReturnsAsync(existingCustomer);
      _customerRepositoryMock.Setup(o => o.GetById(newCustomerId, userId)).ReturnsAsync(createdCustomer);
      _customerRepositoryMock.Setup(o => o.Create(customerToCreate, userId)).ReturnsAsync(newCustomerId);

      var command = new CreateCommand { Customer = customerDto, UserId = userId };

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
      CustomerDto customerDto = null;

      
      var commandResultExpected = CommandResult.Fail("Object reference not set to an instance of an object.", Constants.LOGIC_EXCEPTION_CODE, "");

      Customer existingCustomer = null;

      _customerRepositoryMock.Setup(o => o.GetByIdentificacion("CC12917723", userId)).ReturnsAsync(existingCustomer);
     
      var command = new CreateCommand { Customer = customerDto, UserId = userId };

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
    }


  }
}
