using CustomersTestApi.Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CustomersTestApi.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
  public abstract class Base : ControllerBase
  {

    /// <summary>
    /// Retornars the error500.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    protected ObjectResult Error(string message)
    {
      return StatusCode(500, new
      {
        error = message
      });
    }

    /// <summary>
    /// Retornars the error500.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="message">The message.</param>
    /// <returns></returns>
    protected ObjectResult Error(string code, string message)
    {
      return StatusCode(500, new
      {
        code = code,
        message = message
      });
    }

    /// <summary>
    /// Retornars the error500.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns></returns>
    protected ObjectResult Error(CommandResult result)
    {
      return StatusCode(500, new
      {
        code = result.FailureReasonCode,
        message = result.FailureReasonMessage,
        messageExpetion = result.FailureReasonExcepcion
      });
    }

    /// <summary>
    /// Creates the specified action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="result">The result.</param>
    /// <returns></returns>
    protected ActionResult CreatedResource(string action, CommandResult result) {
      return CreatedAtAction(
        action, 
        new { id = result.Id },
        value: result.Data);
    }
  }
}
