using AutoMapper;
using CustomersTestApi.Domain.Commands;
using CustomersTestApi.Domain.Handlers;
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
  public class DeleteCommandHandlerTests : TestsBase
  {

    /// <summary>
    /// System under Test
    /// </summary>
    private readonly DeleteCommandHandler _sut;

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
    public DeleteCommandHandlerTests()
    {
      _conf = new GeneralConf();
      _generalConfMock.Setup(o => o.Value).Returns(_conf);
      _sut = new DeleteCommandHandler(_mapperMock.Object, _customerRepositoryMock.Object, _generalConfMock.Object);
    }

    /// <summary>
    /// Handles the customer not exists returns error.
    /// </summary>
    [Fact]
    public async Task Handle_CustomerNotExists_ReturnsError()
    {
      //Arrange

      var command = new DeleteCommand
      {
        CustomerId = 1,
        UserId = userId
      };

      Customer oldCustomer = null;

      _customerRepositoryMock.Setup(o => o.GetById(command.CustomerId, userId)).ReturnsAsync(oldCustomer);

      var commandResultExpected = CommandResult.Fail(Messages.OBJECT_NOT_FOUND_MESSAGE
        , Constants.LOGIC_EXCEPTION_CODE, $"CustomerId: {command.CustomerId}");


      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonCode, commandResult.FailureReasonCode);
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
      Assert.Equal(commandResultExpected.FailureReasonExcepcion, commandResult.FailureReasonExcepcion);
    }

    [Fact]
    public async Task Handle_CustomerExistsNotSave_ReturnError() {

      //Arrange
      var command = new DeleteCommand { CustomerId = 1, UserId = userId };

      Customer oldCustomer = GetCustomer();
      oldCustomer.UniqueId = 1;

      _customerRepositoryMock.Setup(o => o.GetById(command.CustomerId, userId)).ReturnsAsync(oldCustomer);

      var commandResultExpected = CommandResult.Fail(Messages.OperationNotSuccessfulMessage, 
        Constants.LOGIC_EXCEPTION_CODE, string.Empty);

      _customerRepositoryMock.Setup(o => o.Delete(command.CustomerId, command.UserId)).ReturnsAsync(0);

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonCode, commandResult.FailureReasonCode);
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
      Assert.Equal(commandResultExpected.FailureReasonExcepcion, commandResult.FailureReasonExcepcion);

    }

    /// <summary>
    /// Handles the customer exists save return nothing.
    /// </summary>
    [Fact]
    public async Task Handle_CustomerExistsSave_ReturnNothing()
    {

      //Arrange
      var command = new DeleteCommand { CustomerId = 1, UserId = userId };

      Customer oldCustomer = GetCustomer();
      oldCustomer.UniqueId = 1;

      _customerRepositoryMock.Setup(o => o.GetById(command.CustomerId, userId)).ReturnsAsync(oldCustomer);

      var commandResultExpected = CommandResult.Success(data: string.Empty);

      _customerRepositoryMock.Setup(o => o.Delete(command.CustomerId, command.UserId)).ReturnsAsync(1);

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonCode, commandResult.FailureReasonCode);
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
      Assert.Equal(commandResultExpected.FailureReasonExcepcion, commandResult.FailureReasonExcepcion);

    }

    /// <summary>
    /// Handles the exception returns error.
    /// </summary>
    [Fact]
    public async Task Handle_Exception_ReturnsError()
    {
      var commandResultExpected = CommandResult.Fail("Object reference not set to an instance of an object.", Constants.LOGIC_EXCEPTION_CODE, "");

      DeleteCommand command = null;

      //Act
      var commandResult = await _sut.Handle(command, new System.Threading.CancellationToken());

      //Assert
      Assert.Equal(commandResultExpected.FailureReasonMessage, commandResult.FailureReasonMessage);
    }

  }
}
