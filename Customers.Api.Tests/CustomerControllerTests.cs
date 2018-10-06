using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Customers.Api.Controllers;
using Customers.DAL;
using Customers.DAL.Models;
using Customers.DTOs;
using Customers.Profiles;
using Customers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Customers.Api.Tests
{
    /// <summary>
    /// Source used for Testing --> https://code-maze.com/unit-testing-aspnetcore-web-api/
    /// </summary>
    public class CustomerControllerTests
    {
        private readonly CustomerDBContext _inMemoryDB;
        private readonly CustomerController _customerController;

        /// <summary>
        /// Setting up required Instances for Unit Tests
        /// </summary>
        public CustomerControllerTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDBContext>();
            optionsBuilder.UseInMemoryDatabase("CustomersMock");
            _inMemoryDB = new CustomerDBContext(optionsBuilder.Options);

            //AutoMapper Unit Testing --> https://stackoverflow.com/questions/49934707/automapper-in-xunit-testing-and-net-core-2-0
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());
            });

            SeedInMemoryDB();

            var logger = new LoggerFactory();
            _customerController = new CustomerController(new CustomerService(_inMemoryDB, mockMapper.CreateMapper(), new LoggerFactory()), new LoggerFactory());
        }

        [Fact]
        public void GetCustomers_WhenCalled_ReturnsOkResult()
        {
            IActionResult okResult = _customerController.Get();
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetCustomerByExistingID_WhenCalled_ReturnsOkResult()
        {
            IActionResult okResult = await _customerController.GetAsync(1);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetCustomerByNotExistingID_WhenCalled_ReturnsNotFoundResult()
        {
            IActionResult notFoundResult = await _customerController.GetAsync(1000);
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task CreateCustomer_WhenCalled_ReturnsCreatedResult()
        {
            var customer = new CustomerDto
            {
                NumberOfEmployees = 10,
                Title = "Vouala",
                CustomerContact = new CustomerContactDto
                { Email = "1@2.com", FirstName = "Chris", LastName = "Bidis" }
            };

            IActionResult createdResult = await _customerController.PostAsync(customer);
            Assert.IsType<CreatedResult>(createdResult);
        }

        /// <summary>
        /// We need integration tests to validate the ModelState  as the Data we send are already deserialized 
        /// </summary>
        /// <returns>This test will always fail</returns>
        [Fact]
        public async Task CreateCustomer_WhenCalled_WithNotValidData_ReturnsBadRequestObjectResult()
        {
            var customer = new CustomerDto
            {
                NumberOfEmployees = 1000,
                Title = "Vouala",
                CustomerContact = new CustomerContactDto
                { Email = "AmIAMail?", FirstName = "Chris", LastName = "Bidis" }
            };

            IActionResult badRequestResult = await _customerController.PostAsync(customer);
            Assert.IsType<BadRequestResult>(badRequestResult);
        }

        [Fact]
        public async Task UpdateCustomer_WhenCalled_WithNotValidIds_ReturnsBadRequestObjectResult()
        {
            IActionResult badRequestResult = await UpdateRequest(2);
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }

        [Fact]
        public async Task UpdateCustomer_WhenCalled_ReturnsAccepteddResult()
        {
            DetachEntity(1);
            IActionResult acceptedResult = await UpdateRequest(1);
            Assert.IsType<AcceptedResult>(acceptedResult);
        }

        [Fact]
        public async Task UpdateCustomer_WhenCalled_WithNotExistingId_ReturnsNotFoundResult()
        {
            IActionResult notFoundResult = await UpdateRequest(1000000, 1000000);
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task DeleteCustomer_WhenCalled_WithNotExistingId_ReturnsNotFoundResult()
        {
            IActionResult notFoundResult = await _customerController.DeleteAsync(99999);
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public async Task DeleteCustomer_WhenCalled_ReturnsNoContentResult()
        {
            DetachEntity(1);
            IActionResult notContentResult = await _customerController.DeleteAsync(1);
            Assert.IsType<NoContentResult>(notContentResult);
        }

        /// <summary>
        /// //In every Test Case the DBContext is a new Instance, 
        /// due to this in order to get a successfull delete we detach the Entity to Update
        /// </summary>
        private void DetachEntity(int keyId) => _inMemoryDB.Entry(_inMemoryDB.Customers.Find(keyId)).State = EntityState.Detached;

        private async Task<IActionResult> UpdateRequest(int urlId, int customerId = 1)
        {
            var existingCustomer = new CustomerDto
            {
                Id = customerId,
                NumberOfEmployees = 10,
                Title = "VoualaUpdated",
                CustomerContact =
                new CustomerContactDto { Id = 1, Email = "1@2.com", FirstName = "ChrisUpdated", LastName = "BidisUpdated" }
            };

            return await _customerController.PutAsync(urlId, existingCustomer);
        }


        /// <summary>
        /// Add Test Data to In Memory DB
        /// </summary>
        private void SeedInMemoryDB()
        {
            var testCustomers = new List<Customer>()
            {
                new Customer{ NumberOfEmployees = 10, Title = "Vouala", CustomerContact =
                new CustomerContact {Email = "1@2.com", FirstName = "Chris", LastName = "Bidis" } },
                new Customer{ NumberOfEmployees = 100, Title = "MyAndMySelf", CustomerContact =
                new CustomerContact {Email = "me@you.com", FirstName = "Me", LastName = "You" } },
                new Customer{ NumberOfEmployees = 0, Title = "HolyDiver", CustomerContact =
                new CustomerContact {Email = "ronnie@james.com", FirstName = "Ronnie", LastName = "Dio" } }
            };

            _inMemoryDB.AddRange(testCustomers);
            _inMemoryDB.SaveChanges();
        }
    }
}
