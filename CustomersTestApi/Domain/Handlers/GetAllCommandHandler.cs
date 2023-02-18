using AutoMapper;
using CustomersTestApi.Domain.Commands;
using CustomersTestApi.Dtos;
using CustomersTestApi.Model;
using CustomersTestApi.Persistance.Repositories;
using CustomersTestApi.Support;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersTestApi.Domain.Handlers
{

  /// <summary>
  /// 
  /// </summary>
  public class GetAllCommandHandler : IRequestHandler<GetAllCommand, CommandResult>
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
    /// Initializes a new instance of the <see cref="GetAllCommandHandler"/> class.
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
    public GetAllCommandHandler(IMapper mapper, ICustomersRepository repository, IOptions<GeneralConf> conf)
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
    public async Task<CommandResult> Handle(GetAllCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var result = await _repository.GetAll(request.UserId);

        var list = new Collection();

        list.Count = result.Count;
        list.List = _mapper.Map<List<CustomerDto>>(result);

        return CommandResult.Success(data: list);
      }
      catch (Exception ex)
      {
        return CommandResult.Fail(ex.Message, Constants.LOGIC_EXCEPTION_CODE, ex.StackTrace);
      }
    }


  }
}
