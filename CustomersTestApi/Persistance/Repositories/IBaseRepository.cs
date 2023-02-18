namespace CustomersTestApi.Persistance.Repositories
{
  /// <summary>
  /// 
  /// </summary>
  public interface IBaseRepository
  {
    /// <summary>
    /// Begins the transaction.
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// Commits the transaction.
    /// </summary>
    void CommitTransaction();

    /// <summary>
    /// Rollbacks the transaction.
    /// </summary>
    void RollbackTransaction();
  }
}