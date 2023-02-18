using AutoMapper;
using CustomersTestApi.Domain.Commands;
using CustomersTestApi.Dtos;
using CustomersTestApi.Model;
using CustomersTestApi.Persistance.Repositories;
using CustomersTestApi.Support;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersTestApi.Domain.Handlers
{
  /// <summary>
  /// 
  /// </summary>
  public class GetByIdCommandHandler : IRequestHandler<GetByIdCommand, CommandResult>
  {
    /// <summary>
    /// The repository
    /// </summary>
    private readonly ICustomersRepository _repository;

    /// <summary>
    /// The mapper
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetByIdCommandHandler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    /// <param name="repository">The repository.</param>
    /// <exception cref="ArgumentNullException">mapper</exception>
    /// <exception cref="ArgumentException">repository</exception>
    public GetByIdCommandHandler(IMapper mapper, ICustomersRepository repository)
    {
      this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      this._repository = repository ?? throw new ArgumentException(nameof(repository));
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Response from the request
    /// </returns>
    public async Task<CommandResult> Handle(GetByIdCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var result = await _repository.GetById(request.Id, request.UserId);

        if(result == null)
          return CommandResult.SuccessWithNoData();
        else
          return CommandResult.Success(data: _mapper.Map<CustomerDto>(result));
      }
      catch (Exception ex)
      {
        return CommandResult.Fail(ex.Message, Constants.LOGIC_EXCEPTION_CODE, ex.StackTrace);
      }
    }
  }
}
