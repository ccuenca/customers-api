namespace CustomersTestApi.Persistance.Repositories
{
  /// <summary>
  /// 
  /// </summary>
  public class BaseRepository : IBaseRepository
  {
    /// <summary>
    /// The database context
    /// </summary>
    protected CustomersContext _dbContext;

    /// <summary>
    /// Gets or sets a value indicating whether [in transaction].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [in transaction]; otherwise, <c>false</c>.
    /// </value>
    private bool InTransaction { get; set; }

    /// <summary>
    /// Begins the transaction asynchronous.
    /// </summary>
    public void BeginTransaction()
    {
      _dbContext.Database.BeginTransaction();
      this.InTransaction = true;
    }

    /// <summary>
    /// Commits the transaction.
    /// </summary>
    public void CommitTransaction()
    {
      _dbContext.Database.CommitTransaction();
      this.InTransaction = false;
    }

    /// <summary>
    /// Rollbacks the transaction.
    /// </summary>
    public void RollbackTransaction()
    {
      if (this.InTransaction)
        _dbContext.Database.RollbackTransaction();
    }
  }
}
