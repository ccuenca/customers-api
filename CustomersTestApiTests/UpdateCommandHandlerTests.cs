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
    /// The user identifier
    /// </summary>
    private int userId = 100;

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

  }
}
