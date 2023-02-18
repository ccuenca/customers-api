using CustomersTestApi.Domain.Commands;
using CustomersTestApi.Dtos;
using CustomersTestApi.Support;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using CustomersTestApi.Model;
using System.Text.Json;
using Serilog;

namespace CustomersTestApi.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
  [Route("api/[controller]")]
  [ApiController]
  public class CustomersController : Base
  {
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// The cache
    /// </summary>
    private readonly IDistributedCache _cache;

    /// <summary>
    /// The cache key
    /// </summary>
    private const string CacheKey = "getall";

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomersController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="cache">The cache.</param>
    /// <exception cref="ArgumentException">
    /// mediator
    /// or
    /// cache
    /// </exception>
    public CustomersController(IMediator mediator, IDistributedCache cache)
    {
      this._mediator = mediator ?? throw new ArgumentException(nameof(mediator));
      this._cache = cache ?? throw new ArgumentException(nameof(cache));
    }

    /// <summary>
    /// Get a customer by id.
    /// </summary>
    /// <param name="command">The id.</param>
    /// <returns></returns>
    [Route("/api/GetById")]
    [HttpGet]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetById([FromQuery] GetByIdCommand command)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var commandResult = await this._mediator.Send(command);

          return commandResult.NoData ? NotFound(new { command.Id }) : (ActionResult) Ok(commandResult.Data);
        }
        else
        {
          return Error(Constants.CONTROLLER__PARAMS_EXCEPTION_CODE, Messages.INVALID_CONTROLLER_PARAMETER);
        }
      }
      catch (Exception ex)
      {
        return Error(Constants.CONTROLLER_EXCEPTION_CODE, ex.Message);
      }
    }

    /// <summary>
    /// Gets the specified command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns></returns>
    [Route("/api/Get")]
    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] GetAllCommand command)
    {
      try
      {
        if (ModelState.IsValid)
        {

          Log.Information("GetAll");
          
          var cacheResult = await _cache.GetStringAsync(CacheKey);

          if (string.IsNullOrEmpty(cacheResult))
          {
            var commandResult = await this._mediator.Send(command);

            if (!commandResult.IsSuccess) return Error(commandResult);
            
            var serializedCustomers = JsonSerializer.Serialize((Collection)commandResult.Data);
            await _cache.SetStringAsync(CacheKey, serializedCustomers);
            return Ok(commandResult.Data);
          }

          var deserializedCustomers = JsonSerializer.Deserialize<Collection>(cacheResult);
          return Ok(deserializedCustomers);
        }
        else
        {
          return Error(Constants.CONTROLLER__PARAMS_EXCEPTION_CODE, Messages.INVALID_CONTROLLER_PARAMETER);
        }
      }
      catch (Exception ex)
      {
        return Error(Constants.CONTROLLER_EXCEPTION_CODE, ex.Message);
      }
    }

    /// <summary>
    /// Create a customer.
    /// </summary>
    /// <param name="command">Customer object to create.</param>
    /// <returns></returns>
    [Route("/api/create")]
    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CreateCommand command)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var commandResult = await this._mediator.Send(command);

          if (!commandResult.IsSuccess) return Error(commandResult);
          await _cache.RemoveAsync(CacheKey);
          return CreatedResource(nameof(GetById), commandResult);
        }
        else
        {
          return Error(Constants.CONTROLLER__PARAMS_EXCEPTION_CODE, Messages.INVALID_CONTROLLER_PARAMETER);
        }
      }
      catch (Exception ex)
      {
        return Error(Constants.CONTROLLER_EXCEPTION_CODE, ex.Message);
      }
    }

    /// <summary>
    /// Update a customer.
    /// </summary>
    /// <param name="command">Customer object with new data.</param>
    /// <returns></returns>
    [Route("/api/update")]
    [HttpPut]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update([FromBody] UpdateCommand command)
    {
      try
      {
        if (ModelState.IsValid)
        {
          if(command.Customer.UniqueId <= 0)
            return Error(Constants.CONTROLLER__PARAMS_EXCEPTION_CODE, $"{Messages.INVALID_CONTROLLER_PARAMETER} - CustomerId is not valid.");

          var commandResult = await this._mediator.Send(command);

          if (!commandResult.IsSuccess) return Error(commandResult);
          await _cache.RemoveAsync(CacheKey);
          return Ok(commandResult.Data);
        }
        else
        {
          return Error(Constants.CONTROLLER__PARAMS_EXCEPTION_CODE, Messages.INVALID_CONTROLLER_PARAMETER);
        }
      }
      catch (Exception ex)
      {
        return Error(Constants.CONTROLLER_EXCEPTION_CODE, ex.Message);
      }
    }

    /// <summary>
    /// Delete a customer.
    /// </summary>
    /// <param name="command">The delete parameters</param>
    /// <returns></returns>
    [Route("/api/delete")]
    [HttpDelete]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete([FromBody] DeleteCommand command)
    {
      try
      {
        if (ModelState.IsValid)
        {
          if (command.CustomerId <= 0)
            return Error(Constants.CONTROLLER__PARAMS_EXCEPTION_CODE, $"{Messages.INVALID_CONTROLLER_PARAMETER} - CustomerId is not valid.");

          var commandResult = await this._mediator.Send(command);

          if (!commandResult.IsSuccess) return Error(commandResult);
          await _cache.RemoveAsync(CacheKey);
          return NoContent();
        }
        else
        {
          return Error(Constants.CONTROLLER__PARAMS_EXCEPTION_CODE, Messages.INVALID_CONTROLLER_PARAMETER);
        }
      }
      catch (Exception ex)
      {
        return Error(Constants.CONTROLLER_EXCEPTION_CODE, ex.Message);
      }
    }
  }
}
