using CustomersTestApi.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersTestApi.Persistance.Repositories
{
  /// <summary>
  /// 
  /// </summary>
  /// <seealso cref="CustomersTestApi.Persistance.Repositories.ICustomersRepository" />
  public class CustomersRepository : BaseRepository, ICustomersRepository
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomersRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <exception cref="ArgumentException">dbContext</exception>
    public CustomersRepository(CustomersContext dbContext)
    {
      this._dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
    }

    /// <summary>
    /// Gets all.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public async Task<List<Customer>> GetAll(int userId)
    {
      var parameters = new SqlParameter[]
      {
        Parameters.CreateInputParameter("@userId", userId,  SqlDbType.Int )
      };

      return await _dbContext.Customers.FromSqlRaw($@"exec {Procs.GET_ALL_PROCEDURE} @userId", parameters).AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Gets the by identifier.
    /// </summary>
    /// <param name="customerId">The customer identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public async Task<Customer> GetById(int customerId, int userId)
    {

      var parameters = new SqlParameter[]
      {
        Parameters.CreateInputParameter("@userId", userId,  SqlDbType.Int ),
        Parameters.CreateInputParameter("@uniqueId", customerId,  SqlDbType.Int )
      };

      var result = await _dbContext.Customers.FromSqlRaw($@"exec {Procs.GET_BY_UNIQUEID_PROCEDURE} @userId,@uniqueId", parameters).AsNoTracking().ToListAsync();

      return result.FirstOrDefault();
    }

    /// <summary>
    /// Creates the specified customer.
    /// </summary>
    /// <param name="customer">The customer.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public async Task<int> Create(Customer customer, int userId)
    {
      var parameters = new SqlParameter[]
      {
        Parameters.CreateInputParameter("@userId", userId,  SqlDbType.Int ),
        Parameters.CreateInputParameter("@names", customer.Names,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@sureNames", customer.SureNames,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@birthDate", customer.BirthDate,  SqlDbType.SmallDateTime),
        Parameters.CreateInputParameter("@phoneNumber", customer.PhoneNumber,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@address", customer.Address,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@emailAddress", customer.EmailAddress,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@identificationNumber", customer.IdentificationNumber,  SqlDbType.NVarChar),
        Parameters.CreateOutputParameter("@result", SqlDbType.Int)
      };

      await _dbContext.Database.ExecuteSqlRawAsync(
        $@"exec {Procs.CREATE_PROCEDURE} 
              @userId,@names,@sureNames,@birthDate,@phoneNumber,@address,@emailAddress,@identificationNumber,@result OUT", parameters);

      return Convert.ToInt32(parameters.Last().Value);
    }

    /// <summary>
    /// Updates the specified customer.
    /// </summary>
    /// <param name="customer">The customer.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public async Task<int> Update(Customer customer, int userId)
    {
      var parameters = new SqlParameter[]
      {
        Parameters.CreateInputParameter("@userId", userId,  SqlDbType.Int ),
        Parameters.CreateInputParameter("@uniqueId", customer.UniqueId,  SqlDbType.Int),
        Parameters.CreateInputParameter("@names", customer.Names,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@sureNames", customer.SureNames,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@birthDate", customer.BirthDate,  SqlDbType.SmallDateTime),
        Parameters.CreateInputParameter("@phoneNumber", customer.PhoneNumber,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@address", customer.Address,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@emailAddress", customer.EmailAddress,  SqlDbType.NVarChar),
        Parameters.CreateInputParameter("@identificationNumber", customer.IdentificationNumber,  SqlDbType.NVarChar),
        Parameters.CreateOutputParameter("@result", SqlDbType.Int)
      };

      await _dbContext.Database.ExecuteSqlRawAsync(
        $@"exec {Procs.UPDATE_PROCEDURE} 
              @userId,@uniqueId,@names,@sureNames,@birthDate,@phoneNumber,@address,@emailAddress,@identificationNumber,@result OUT", parameters);

      return Convert.ToInt32(parameters.Last().Value);
    }

    /// <summary>
    /// Deletes the specified customer identifier.
    /// </summary>
    /// <param name="customerId">The customer identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public async Task<int> Delete(int customerId, int userId)
    {
      var parameters = new SqlParameter[]
      {
        Parameters.CreateInputParameter("@userId", userId,  SqlDbType.Int ),
        Parameters.CreateInputParameter("@uniqueId", customerId,  SqlDbType.Int),
        Parameters.CreateOutputParameter("@result", SqlDbType.Int)
      };

      await _dbContext.Database.ExecuteSqlRawAsync(
        $@"exec {Procs.DELETE_PROCEDURE} 
              @userId,@uniqueId,@result OUT", parameters);

      return Convert.ToInt32(parameters.Last().Value);
    }

    /// <summary>
    /// Gets the by identificacion.
    /// </summary>
    /// <param name="identification">The identification.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public async Task<Customer> GetByIdentificacion(string identification, int userId)
    {
      var parameters = new SqlParameter[]
      {
        Parameters.CreateInputParameter("@userId", userId,  SqlDbType.Int ),
        Parameters.CreateInputParameter("@identificaionNumber", identification,  SqlDbType.NVarChar )
      };

      var result = await _dbContext.Customers.FromSqlRaw($@"exec {Procs.GET_BY_IDENTIFICATION_PROCEDURE} @userId,@identificaionNumber", parameters).AsNoTracking().ToListAsync();

      return result.FirstOrDefault();
    }
  }

}



