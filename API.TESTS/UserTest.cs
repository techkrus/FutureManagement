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
using System.Collections.Generic;
using AutoMapper;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.TESTS
{
    public class UserTest : IDisposable
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper; 
        private readonly IUserRepository _repo;

        public UserTest(){
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

            _repo = new UserRepository(_dbContext);
            MapperConfiguration config = new MapperConfiguration( cfg => {
                cfg.CreateMap<ItemTemplate, ItemTemplateForGetDto>();
                cfg.CreateMap<ItemTemplate, ItemTemplateForAddDto>();
                cfg.CreateMap<ItemTemplate, ItemTemplateForTableDto>();

            });
            
            _mapper =  config.CreateMapper();
        }
                        
        [Fact]
        public async void GetAllActiveUsersTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            // Act
            IActionResult allActiveUsers = await con.GetAllActiveUsers();
            OkObjectResult intermediate = allActiveUsers as OkObjectResult;
            List<UserForGetDto> result = intermediate.Value as List<UserForGetDto>;
            // Assert
            Assert.True(result.Count == 1);
        }
        [Fact]
        public async void GetAllInactiveUsersTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            // Act
            IActionResult allActiveUsers = await con.GetAllInactiveUsers();
            OkObjectResult intermediate = allActiveUsers as OkObjectResult;
            List<UserForGetDto> result = intermediate.Value as List<UserForGetDto>;
            // Assert
            Assert.True(result.Count == 1);
        }
        [Fact]
        public async void GetUserTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);

            // Act
            OkObjectResult intermediate = await con.GetUser(1) as OkObjectResult;
            UserForGetDto userToGet = intermediate.Value as UserForGetDto;

            // Assert
            Assert.True(userToGet.Username == "Tekrus");
        }
        [Fact]
        public async void AddRoleTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);

            // Act
            await con.AddNewRole("Developer");

            // Assert
            var role = _dbContext.UserRoles.FirstOrDefault(x => x.Name == "Developer");
            Assert.NotNull(role);

        }
        [Fact]
        public async void EditUserTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            var testUser = _dbContext.Users.FirstOrDefault(x => x.Id == 1);
            // Act
            testUser.Phone = "52442312";
            await con.EditUser(testUser);
            var editedTestUser = _dbContext.Users.FirstOrDefault(x => x.Id == 1);
            // Assert
            Assert.True(editedTestUser.Phone == testUser.Phone);                          
        }                                                     
        [Fact]
        public async void EditUserReturnTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            var testUser = _dbContext.Users.FirstOrDefault(x => x.Id == 1);
            // Act
            testUser.Phone = "52442312";
            var status = await con.EditUser(testUser);
            // Assert
            StatusCodeResult result = status as StatusCodeResult;
            var test = new StatusCodeResult(200);           
            Assert.True(result.StatusCode == test.StatusCode);                          
        }
        [Fact]
        public async void DeactivateUserTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            var testUser = _dbContext.Users.FirstOrDefault(x => x.Id == 1);
            // Act
            await con.DeactivateUser(testUser.Id);
            User deactivatedUser = _dbContext.Users.FirstOrDefault(x => x.Id == 1);
            // Assert
            Assert.True(deactivatedUser.IsActive == false);
                                      
        } 
        [Fact]
        public async void DeactivateUserReturnTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            var testUser = _dbContext.Users.FirstOrDefault(x => x.Id == 1);
            // Act
            var status = await con.DeactivateUser(testUser.Id);
            // Assert
            StatusCodeResult result = status as StatusCodeResult;
            var test = new StatusCodeResult(200);           
            Assert.True(result.StatusCode == test.StatusCode);                          
        }
        [Fact]
        public async void ActivateUserTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            var testUser = _dbContext.Users.FirstOrDefault(x => x.Id == 2);
            // Act
            await con.ActivateUser(testUser.Id);
            User activatedUser = _dbContext.Users.FirstOrDefault(x => x.Id == 2);
            // Assert
            Assert.True(activatedUser.IsActive == true);
                                      
        } 
        [Fact]
        public async void ActivateUserReturnTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            var testUser = _dbContext.Users.FirstOrDefault(x => x.Id == 2);
            // Act
            var status = await con.DeactivateUser(testUser.Id);
            // Assert
            StatusCodeResult result = status as StatusCodeResult;
            var test = new StatusCodeResult(200);           
            Assert.True(result.StatusCode == test.StatusCode);                          
        }
        [Fact]
        public async void DeleteUserTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            var testUser = _dbContext.Users.FirstOrDefault(x => x.Id == 2);
            // Act
            await con.DeleteUser(testUser.Id);
            User deactivatedUser = _dbContext.Users.FirstOrDefault(x => x.Id == 2);
            // Assert
            Assert.Null(deactivatedUser);
                                      
        } 
        [Fact]
        public async void DeleteUserReturnTest(){
            // Arrange
            var con = new UserController(_repo, _dbContext, _mapper);
            var testUser = _dbContext.Users.FirstOrDefault(x => x.Id == 2);
            // Act
            var status = await con.DeleteUser(testUser.Id);
            // Assert
            StatusCodeResult result = status as StatusCodeResult;
            var test = new StatusCodeResult(200);           
            Assert.True(result.StatusCode == test.StatusCode);                          
        }
        [Fact]
        public void EditUserRoleTest(){
            
        }
           
        private void Seed(DataContext context){
            var user1 = new User(
                "Tekrus",
                new UserRole("Old man"),
                "Daniel",
                "måsgaardson",
                DateTime.MinValue,
                true,
                "måsIGaard3@mor.fae",
                "12456790"
            );
            var user2 = new User(
                "Fatdet",
                new UserRole("Lead program developer"),
                "Frederik",
                "The Champ",
                DateTime.MaxValue,
                false,
                "freder@gmail.com",
                "93208854"
            );

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();

        }
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}