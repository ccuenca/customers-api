using AutoMapper;
using CustomersTestApi.Domain.Commands;
using CustomersTestApi.Dtos;
using CustomersTestApi.Model;
using CustomersTestApi.Persistance.Repositories;
using CustomersTestApi.Support;
using MediatR;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersTestApi.Domain.Handlers
{
  /// <summary>
  /// 
  /// </summary>
  public class CreateCommandHandler : IRequestHandler<CreateCommand, CommandResult>
  {
    /// <summary>
    /// The repository
    /// </summary>
    private readonly ICustomersRepository _repository;

    /// <summary>
    /// The general conf
    /// </summary>
    private readonly GeneralConf _generalConf;

    /// <summary>
    /// The mapper
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCommandHandler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    /// <param name="repository">The repository.</param>
    /// <param name="conf">The conf.</param>
    /// <exception cref="ArgumentNullException">
    /// mapper
    /// or
    /// conf
    /// </exception>
    /// <exception cref="ArgumentException">repository</exception>
    public CreateCommandHandler(IMapper mapper, ICustomersRepository repository, IOptions<GeneralConf> conf)
    {
      this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      this._repository = repository ?? throw new ArgumentException(nameof(repository));
      this._generalConf = conf.Value ?? throw new ArgumentNullException(nameof(conf));
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Response from the request
    /// </returns>
    public async Task<CommandResult> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var existingCustomer = await _repository.GetByIdentificacion(
          request.Customer.IdentificationNumber.Trim(), request.UserId);

        if (existingCustomer != null)
        {
          Log.Information(Constants.LOGIC_EXCEPTION_CODE + "-" + Messages.ObjectExists
                          + $" - CustomerIdentification: {request.Customer.IdentificationNumber}"
                          + " - data: {@request} " + nameof(CreateCommandHandler), request);

          return CommandResult.Fail(Messages.ObjectExists, Constants.LOGIC_EXCEPTION_CODE,
            $"CustomerIdentification: {request.Customer.IdentificationNumber}");
        }

        var result = await _repository.Create(_mapper.Map<Customer>(request.Customer), request.UserId);

        if (result == 0)
        {
          Log.Information(Constants.LOGIC_EXCEPTION_CODE + "-" + Messages.OperationNotSuccessfulMessage
                          + $" - CustomerIdentification: {request.Customer.IdentificationNumber}"
                          + " - data: {@request} " + nameof(CreateCommandHandler), request);

          return CommandResult.Fail(Messages.OperationNotSuccessfulMessage, Constants.LOGIC_EXCEPTION_CODE, string.Empty);
        }


        var customer = await _repository.GetById(result, request.UserId);

        if (customer == null)
        {
          Log.Information(Constants.LOGIC_EXCEPTION_CODE + "-" + Messages.ObjectExists
                          + $" - CustomerIdentification: {request.Customer.IdentificationNumber}"
                          + " - data: {@request} " + nameof(CreateCommandHandler), request);

          return CommandResult.Fail(Messages.OBJECT_NOT_FOUND_MESSAGE,
            Constants.LOGIC_EXCEPTION_CODE, $"CustomerId: {result}");
        }


        Log.Information(Messages.CUSTOMER_CREATED
                          + $" - CustomerIdentification: {request.Customer.IdentificationNumber}"
                          + " - data: {@request} " + nameof(CreateCommandHandler), request);

        return CommandResult.Success(data: _mapper.Map<CustomerDto>(customer), id: customer.UniqueId);
      }
      catch (Exception ex)
      {
        return CommandResult.Fail(ex.Message, Constants.LOGIC_EXCEPTION_CODE, ex.StackTrace);
      }
    }


  }
}
