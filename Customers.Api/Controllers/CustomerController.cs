using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Customers.Api.Models;
using Customers.DTOs;
using Customers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IOperations<CustomerDto> _customerService;

        public CustomerController(IOperations<CustomerDto> customerService) => _customerService = customerService;

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CustomerDto>>), 200)]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<CustomerDto> customers = _customerService.GetAll();
                return Ok(new ApiResponse<IEnumerable<CustomerDto>>(customers));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CustomerDto>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                CustomerDto customer = await _customerService.GetByIdAsync(id);
                return Ok(new ApiResponse<CustomerDto>(customer));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CustomerDto customer) => Ok(await _customerService.InsertAsync(customer));

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _customerService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
