using CustomersTestApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomersTestApi.Persistance.Repositories
{
  /// <summary>
  /// 
  /// </summary>
  public interface ICustomersRepository : IBaseRepository
  {
    /// <summary>
    /// Gets all.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    Task<List<Customer>> GetAll(int userId);

    /// <summary>
    /// Gets the by identifier.
    /// </summary>
    /// <param name="customerId">The customer identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    Task<Customer> GetById(int customerId, int userId);

    /// <summary>
    /// Creates the specified customer.
    /// </summary>
    /// <param name="customer">The customer.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    Task<int> Create(Customer customer, int userId);

    /// <summary>
    /// Updates the specified customer.
    /// </summary>
    /// <param name="customer">The customer.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    Task<int> Update(Customer customer, int userId);

    /// <summary>
    /// Deletes the specified customer identifier.
    /// </summary>
    /// <param name="customerId">The customer identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    Task<int> Delete(int customerId, int userId);

    /// <summary>
    /// Gets the by identificacion.
    /// </summary>
    /// <param name="identification">The identification.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    Task<Customer> GetByIdentificacion(string identification, int userId);

  }
}
