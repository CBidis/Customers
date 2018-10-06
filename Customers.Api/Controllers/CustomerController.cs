using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customers.Api.Models;
using Customers.DTOs;
using Customers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Customers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IOperations<CustomerDto> _customerService;
        private readonly ILogger _logger;

        public CustomerController(IOperations<CustomerDto> customerService, ILoggerFactory loggerFactory)
        {
            _customerService = customerService;
            _logger = loggerFactory.CreateLogger<CustomerController>();
        } 

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CustomerDto>>), 200)]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<CustomerDto> customers = _customerService.GetAll();
                return Ok(new ApiResponse<IEnumerable<CustomerDto>>(customers));
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>(ex.Message));
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
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>(ex.Message));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<IActionResult> PostAsync([FromBody] CustomerDto customer)
        {
            try
            {
                var createdId = await _customerService.InsertAsync(customer);
                return Created($"api/customer/{createdId}", createdId);
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 202)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CustomerDto customer)
        {
            try
            {
                if (id != customer.Id)
                    return BadRequest(new ApiResponse<string>($"Url Param {id} is not the Same as the requested object {customer?.Id}"));

                await _customerService.UpdateAsync(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>(ex.Message));
            }
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
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>(ex.Message));
            }
        }
    }
}
