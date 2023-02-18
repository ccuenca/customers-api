using AutoMapper;
using CustomersTestApi.Domain.Commands;
using CustomersTestApi.Persistance.Repositories;
using CustomersTestApi.Support;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersTestApi.Domain.Handlers
{
  /// <summary>
  /// 
  /// </summary>
  public class DeleteCommandHandler : IRequestHandler<DeleteCommand, CommandResult>
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
    /// Initializes a new instance of the <see cref="DeleteCommandHandler"/> class.
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
    public DeleteCommandHandler(IMapper mapper, ICustomersRepository repository, IOptions<GeneralConf> conf)
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
    public async Task<CommandResult> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var customer = await _repository.GetById(request.CustomerId, request.UserId);

        if (customer == null)
          return CommandResult.Fail(Messages.OBJECT_NOT_FOUND_MESSAGE, Constants.LOGIC_EXCEPTION_CODE, $"CustomerId: {request.CustomerId}");

        var result = await _repository.Delete(request.CustomerId, request.UserId);

        if (result == 0)
          return CommandResult.Fail(Messages.OperationNotSuccessfulMessage, Constants.LOGIC_EXCEPTION_CODE, string.Empty);

        return CommandResult.Success(data: string.Empty);
      }
      catch (Exception ex)
      {
        return CommandResult.Fail(ex.Message, Constants.LOGIC_EXCEPTION_CODE, ex.StackTrace);
      }
    }
  }
}
