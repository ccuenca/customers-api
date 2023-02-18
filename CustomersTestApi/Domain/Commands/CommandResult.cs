 namespace CustomersTestApi.Domain.Commands
{
  /// <summary>
  /// 
  /// </summary>
  public class CommandResult
  {
    /// <summary>
    /// Constructor por defecto.
    /// </summary>
    private CommandResult() { }

    /// <summary>
    /// Constructor con razón de fallo.
    /// </summary>
    /// <param name="failureReason">Razón de fallo</param>
    private CommandResult(string failureReason)
    {
      FailureReasonMessage = failureReason;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandResult"/> class.
    /// </summary>
    /// <param name="failureReason">The failure reason.</param>
    /// <param name="code">The code.</param>
    private CommandResult(string failureReason, string code)
    {
      FailureReasonMessage = failureReason;
      FailureReasonCode = code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandResult"/> class.
    /// </summary>
    /// <param name="failureReason">The failure reason.</param>
    /// <param name="code">The code.</param>
    /// <param name="failureException">The failure exception.</param>
    private CommandResult(string failureReason, string code, string failureException)
    {
      FailureReasonMessage = failureReason;
      FailureReasonCode = code;
      FailureReasonExcepcion = failureException;
    }

    /// <summary>
    /// Propiedad que indica la Razón de fallo
    /// </summary>
    public string FailureReasonMessage { get; }

    /// <summary>
    /// Gets or sets a value indicating whether [no data].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [no data]; otherwise, <c>false</c>.
    /// </value>
    public bool NoData { get; set; }

    /// <summary>
    /// Propiedad que indica la Razón de fallo
    /// </summary>
    public string FailureReasonCode { get; }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public dynamic Id { get; set; }

    /// <summary>
    /// Datos resultado de la ejecución del comando.
    /// </summary>
    public dynamic Data { get; set; }

    /// <summary>
    /// Propiedad que indica la Razón de fallo mas detallada
    /// </summary>
    public string FailureReasonExcepcion { get; }

    /// <summary>
    /// Propiedad que indica si la ejecución fue correcta.
    /// </summary>
    public bool IsSuccess => string.IsNullOrWhiteSpace(FailureReasonMessage);

    /// <summary>
    /// Metodo estatico que crea un resultado exitoso.
    /// </summary>
    /// <returns></returns>
    public static CommandResult Success()
    {
      return new CommandResult();
    }


    /// <summary>
    /// Metodo estatico que crea un resultado exitoso con datos de resultado.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static CommandResult Success(dynamic data)
    {
      return new CommandResult()
      {
        Data = data
      };
    }

    /// <summary>
    /// Successes the specified data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public static CommandResult Success(dynamic data, dynamic id)
    {
      return new CommandResult()
      {
        Data = data,
        Id = id
      };
    }

    /// <summary>
    /// Successes the with no data.
    /// </summary>
    /// <returns></returns>
    public static CommandResult SuccessWithNoData()
    {
      return new CommandResult()
      {
        NoData = true
      };
    }

    /// <summary>
    /// Metodo estatico que crea un resultado fallido con razón de fallo.
    /// </summary>
    /// <param name="reason"></param>
    /// <returns></returns>
    public static CommandResult Fail(string reason)
    {
      return new CommandResult(reason);
    }

    /// <summary>
    /// Metodo estatico que crea un resultado fallido con razón de fallo.
    /// </summary>
    /// <param name="reason"></param>
    /// /// <param name="code"></param>
    /// <returns></returns>
    public static CommandResult Fail(string reason, string code)
    {
      return new CommandResult(reason, code);
    }

    /// <summary>
    /// Metodo estatico que crea un resultado fallido con razón de fallo.
    /// </summary>
    /// <param name="reason"></param>
    /// <param name="code"></param>
    /// <param name="reasonExcepcion"></param>
    /// <returns></returns>
    public static CommandResult Fail(string reason, string code, string reasonExcepcion)
    {
      return new CommandResult(reason, code, reasonExcepcion);
    }

    /// <summary>
    /// Metodo para comparación.
    /// </summary>
    /// <param name="result"></param>
    public static implicit operator bool(CommandResult result)
    {
      return result.IsSuccess;
    }
  }
}
