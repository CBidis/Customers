using AutoMapper;
using Customers.DAL;
using Customers.DAL.Models;
using Customers.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customers.Services
{
    /// <summary>
    /// Implements Common Operations upon the Table of Customers
    /// </summary>
    public class CustomerService : IOperations<CustomerDto>
    {
        private readonly CustomerDBContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerService(CustomerDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<CustomerDto> GetAll() => _mapper.Map<IEnumerable<CustomerDto>>(_dbContext.Customers.Include(c => c.CustomerContact));

        public async Task<CustomerDto> GetByIdAsync(int id) => _mapper.Map<CustomerDto>(await GetCustomerByIdAsync(id));

        public async Task<int> DeleteAsync(int id)
        {
            Customer customer = await GetCustomerByIdAsync(id);

            if (customer == null)
                throw new ArgumentNullException($"There is no Customer with Id {id}");

            _dbContext.Remove(customer);
            return await _dbContext.SaveChangesAsync();
        }
        
        public async Task<int> InsertAsync(CustomerDto customerDto)
        {
            Customer customerToAdd = _mapper.Map<Customer>(customerDto);
            await _dbContext.AddAsync(customerToAdd);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(CustomerDto customerDto)
        {
            Customer customer = await GetCustomerByIdAsync(customerDto?.Id ?? 0);

            if (customer == null)
                throw new ArgumentNullException($"There is no Customer with Title {customerDto?.Title}");

            _dbContext.Update(customer);
            return await _dbContext.SaveChangesAsync();
        }

        private async Task<Customer> GetCustomerByIdAsync(int id) => await _dbContext.Customers.Include(c => c.CustomerContact).FirstOrDefaultAsync(p => p.Id == id);
    }
}
