using AutoMapper;
using Customers.DAL.Models;
using Customers.DTOs;

namespace Customers.Profiles
{
    /// <summary>
    /// Creating Maps for AutoMapper
    /// </summary>
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, Customer>().ForMember(x => x.CustomerContact, opt => opt.MapFrom(src => new CustomerContact
            {
                Id = src.CustomerContact.Id,
                FirstName = src.CustomerContact.FirstName,
                LastName = src.CustomerContact.LastName,
                Email = src.CustomerContact.Email
            }));

            CreateMap<Customer, CustomerDto>().ForMember(x => x.CustomerContact, opt => opt.MapFrom(src => new CustomerContactDto
            {
                Id = src.CustomerContact.Id,
                FirstName = src.CustomerContact.FirstName,
                LastName = src.CustomerContact.LastName,
                Email = src.CustomerContact.Email
            }));
        }
    }
}
