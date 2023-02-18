﻿using CustomersTestApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomersTestApi.Persistance
{
  /// <summary>
  /// 
  /// </summary>
  /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
  public class CustomersContext : DbContext
  {
    /// <summary>
    /// Gets or sets the customers.
    /// </summary>
    /// <value>
    /// The customers.
    /// </value>
    public virtual DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomersContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public CustomersContext(DbContextOptions options) : base(options) { }

    /// <summary>
    /// Override this method to further configure the model that was discovered by convention from the entity types
    /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
    /// and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
    /// define extension methods on this object that allow you to configure aspects of the model that are specific
    /// to a given database.</param>
    /// <remarks>
    /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    /// then this method will not be run.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Customer>(entity => entity.HasKey(e => e.UniqueId));

    }
  }
}
