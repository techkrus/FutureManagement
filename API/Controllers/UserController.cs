using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _repo;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UserController(IUserRepository repo, DataContext context, IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _repo = repo;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Policy = "User_View")]
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveUsers()
        {
            var users = await _repo.GetAllActiveUsers();
            var usersToReturn = _mapper.Map<List<UserForGetDto>>(users);

            return Ok(usersToReturn);
        }

        [Authorize(Policy = "User_View")]
        [HttpGet("inactive")]
        public async Task<IActionResult> GetAllInactiveUsers()
        {
            var users = await _repo.GetAllInactiveUsers();
            var usersToReturn = _mapper.Map<List<UserForGetDto>>(users);

            return Ok(usersToReturn);

        }

        [Authorize(Policy = "User_View")]
        [HttpGet("get/{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await _repo.GetUser(id);
            UserForGetDto userToReturn = _mapper.Map<UserForGetDto>(user);

            return Ok(userToReturn);            
        }

        [Authorize(Policy = "User_Edit")]
        [HttpPost("edit")]
        public async Task<IActionResult> EditUser([FromBody]User user)
        {
            if(user.Id == 0){
                ModelState.AddModelError("User Error", "User id can not be 0.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool succes = await _repo.EditUser(user);

            return succes ? StatusCode(200) : BadRequest();

        }
        
        [Authorize(Policy = "User_View")]      
        [HttpGet("GetUsersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles(){
            var userList = await (from user in _context.Users orderby user.UserName
                                    select new
                                    {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        Roles = (from userRole in user.UserRoles
                                                    join role in _context.Roles
                                                    on userRole.RoleId
                                                    equals role.Id
                                                    select role.Name).ToList()
                                                    
                                    }).ToListAsync();
            return Ok(userList);
        }

        [Authorize(Policy = "User_ActivateDeactivate")]
        [HttpPost("deactivate/{id}", Name = "DeactivateUser")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var user = await _repo.GetUser(id);
            user.IsActive = false;
            var succes = await _repo.DeActivateUser(user);
            return succes ? StatusCode(200) : BadRequest();
        }

        [Authorize(Policy = "User_ActivateDeactivate")]
        [HttpPost("activate")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var userActivate = await _repo.GetUser(id);
            userActivate.IsActive = true;
            bool succes = await _repo.ActivateUser(userActivate);
            return succes ? StatusCode(200) : BadRequest();
        }

        [Authorize(Policy = "User_Add")]
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody]User userToAdd)
        {
            // TODO maybe need to be UserForRegister or something
            bool succes = await _repo.AddUser(userToAdd);
            return succes ? StatusCode(200) : BadRequest();
        }

        [Authorize(Policy = "User_Edit")]
        [HttpPost("EditRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName,[FromBody] RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;
            selectedRoles = selectedRoles ?? new string[]{};
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if(!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if(!result.Succeeded)
                return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "User_Add")]
        [HttpPost("addRole")]
        public async Task<IActionResult> AddNewRole([FromBody]string name)
        {   
            if(name != null){
                bool roleCheck = await _roleManager.RoleExistsAsync(name.ToUpper());
                if (!roleCheck){
                    var result = _roleManager.CreateAsync(new Role{Name = name}).Result;
                    if(result.Succeeded){
                        return StatusCode(201);
                    }
                }
            }
            
            return BadRequest();
        }


    }
}