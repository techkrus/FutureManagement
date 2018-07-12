using System;
using Xunit;
using API;
using API.Models;
using API.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using API.Enums;
using API.Data;

namespace API.TESTS
{
    public class OrderTest : IDisposable
    {
        private readonly DataContext _dbContext;

        public OrderTest(){
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            _dbContext = new DataContext(options);
            _dbContext.Database.EnsureCreated();
            Seed(_dbContext);
        }

        private void Seed(DataContext context){
            var orders = new[]{
                new Order("CompanyA", 
                    DateTime.Today, 
                    DateTime.Now, 
                    null, 
                    "core/pathA.pdf", 
                    1, 
                    123, 
                    456, 
                    789, 
                    UnitType.cm),
                new Order("CompanyB", 
                    DateTime.Today, 
                    DateTime.Now, 
                    null, 
                    "core/pathB.pdf", 
                    2, 
                    345, 
                    678, 
                    910, 
                    UnitType.mm),
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }

        [Fact]
        private async void ReadOrderReturnsCorrectResult(){
            // Arrange
            var controller = new OrderController(_dbContext);

            // Act
            var result = await controller.GetOrder(1);

            // Assert
            Assert.True(result.Id == 1);
        }

        [Fact]
        private async void ReadOrderReturnsFalseResult(){
            // Arrange
            var controller = new OrderController(_dbContext);

            // Act
            var result = await controller.GetOrder(2);

            // Assert
            Assert.False(result.Id == 1);
        }

        [Fact]
        private async void InsertOrderSuccessfully(){
            // Arange
            var controller = new OrderController(_dbContext);
            var testOrder = new Order();

            // Act
            var result = await controller.CreateOrder(testOrder);

            // Assert
            Assert.False(result);
        }

        [Fact]
        private async void InsertOrderUnsuccessfully(){
            // Arange
            var controller = new OrderController(_dbContext);
            var testOrder = new Order(
                    "CompanyC", 
                    DateTime.Today, 
                    DateTime.Now, 
                    null, 
                    "core/pathA.pdf", 
                    1, 
                    486, 
                    153, 
                    125, 
                    UnitType.cm
                );

            // Act
            var result = await controller.CreateOrder(testOrder);

            // Assert
            Assert.True(result);
        }

        [Fact]
        private async void GetAllOrdersGetsAllOrders(){
            // Arrange
            var controller = new OrderController(_dbContext);

            // Act
            var result = await controller.GetAllOrders();

            // Assert
            Assert.Equal(result.Count, 2);
        }

        public void Dispose(){
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}