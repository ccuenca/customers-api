using AutoMapper;
using CustomersTestApi.Dtos;

namespace CustomersTestApi.Model
{
  /// <summary>
  /// 
  /// </summary>
  /// <seealso cref="AutoMapper.Profile" />
  public class MappingProfile : Profile
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// </summary>
    public MappingProfile()
    { 
      CreateMap<Customer, CustomerDto>();
      CreateMap<CustomerDto, Customer>();
    }

  }
}
