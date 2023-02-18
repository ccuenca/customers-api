using Microsoft.Data.SqlClient;
using System.Data;

namespace CustomersTestApi.Persistance
{
  /// <summary>
  /// 
  /// </summary>
  public class Parameters
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Parameters"/> class.
    /// </summary>
    protected Parameters()
    {

    }
    /// <summary>
    /// Creates the input parameter.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static SqlParameter CreateInputParameter(string name, object value, SqlDbType type)
    {
      return new SqlParameter
      {
        ParameterName = name,
        SqlDbType = type,
        Direction = ParameterDirection.Input,
        Value = value
      };
    }

    /// <summary>
    /// Creates the output parameter.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static SqlParameter CreateOutputParameter(string name, SqlDbType type)
    {
      return new SqlParameter
      {
        ParameterName = name,
        SqlDbType = type,
        Direction = ParameterDirection.Output
      };
    }
  }
}
