using AutoMapper;
using Customers.DAL;
using Customers.DAL.Models;
using Customers.DTOs;
using Customers.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CustomerService(CustomerDBContext dbContext, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<CustomerService>();
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            try
            {
                var allCustomers = _dbContext.Customers.AsNoTracking().Include(c => c.CustomerContact);
                return _mapper.Map<IEnumerable<CustomerDto>>(allCustomers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred");
                throw;
            }
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            try
            {
                Customer customer = await GetCustomerByIdAsync(id);

                if (customer == null)
                    throw new EntityNotFoundException($"There is no Customer with Id {id}");

                return _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred");
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                Customer customer = await GetCustomerByIdAsync(id);

                if (customer == null)
                    throw new EntityNotFoundException($"There is no Customer with Id {id}");

                _dbContext.Remove(customer);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred");
                throw;
            }
        }
        
        public async Task<int> InsertAsync(CustomerDto customerDto)
        {
            try
            {
                Customer customerToAdd = _mapper.Map<Customer>(customerDto);
                await _dbContext.AddAsync(customerToAdd);
                await _dbContext.SaveChangesAsync();
                return customerToAdd.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred");
                throw;
            }
        }

        public async Task<int> UpdateAsync(CustomerDto customerDto)
        {
            try
            {
                Customer customer = await GetCustomerByIdAsync(customerDto?.Id ?? 0);

                if (customer == null)
                    throw new EntityNotFoundException($"There is no Customer with Title {customerDto?.Title}");

                Customer customerToUpdate = _mapper.Map<Customer>(customerDto);
                _dbContext.Update(customerToUpdate);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred");
                throw;
            }
        }

        private async Task<Customer> GetCustomerByIdAsync(int id) => await _dbContext.Customers.Include(c => c.CustomerContact).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
}
