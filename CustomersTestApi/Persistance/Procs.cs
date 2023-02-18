namespace CustomersTestApi.Persistance
{
  /// <summary>
  /// Class to define constants for stored procedures names
  /// </summary>
  public class Procs
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="Procs"/> class.
    /// </summary>
    protected Procs()
    {

    }

    /// <summary>
    /// The get all procedure
    /// </summary>
    internal static readonly string GET_ALL_PROCEDURE = "stpGetAllCustomers";

    /// <summary>
    /// The get by uniqueid procedure
    /// </summary>
    internal static readonly string GET_BY_UNIQUEID_PROCEDURE = "stpGetCustomerByUniqueId";

    /// <summary>
    /// The create procedure
    /// </summary>
    internal static readonly string CREATE_PROCEDURE = "stpCreateCustomer";

    /// <summary>
    /// The update procedure
    /// </summary>
    internal static readonly object UPDATE_PROCEDURE = "stpUpdateCustomer";

    /// <summary>
    /// The delete procedure
    /// </summary>
    internal static readonly object DELETE_PROCEDURE = "stpDeleteCustomer";

    /// <summary>
    /// The get by identification procedure/
    /// </summary>
    internal static readonly object GET_BY_IDENTIFICATION_PROCEDURE = "stpGetCustomerByIdentification";

    
  }
}
