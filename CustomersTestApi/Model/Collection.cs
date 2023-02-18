using System;

namespace CustomersTestApi.Model
{
  /// <summary>
  /// 
  /// </summary>
  [Serializable]
  public class Collection
  {

    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public dynamic List { get; set; }



  }
}
