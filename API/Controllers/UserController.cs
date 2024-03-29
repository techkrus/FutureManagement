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
using API.Enums;

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
        private readonly IEventLogRepository _eventLogRepo;
        public UserController(IUserRepository repo, DataContext context, IMapper mapper, UserManager<User> userManager, 
                                RoleManager<Role> roleManager, IEventLogRepository eventLogRepo)
        {
            _context = context;
            _mapper = mapper;
            _repo = repo;
            _userManager = userManager;
            _roleManager = roleManager;
            _eventLogRepo = eventLogRepo;
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

        [Authorize()]
        [HttpGet("get/my", Name = "GetMyUser")]
        public async Task<IActionResult> GetMyUser()
        {
            
            User currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            User user = await _repo.GetUser(currentUser.Id);
            UserForGetDto userToReturn = _mapper.Map<UserForGetDto>(user);

            return Ok(userToReturn);            
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
            var userToChange = _context.Users.First(x => x.Id == user.Id);

            user.AccessFailedCount = userToChange.AccessFailedCount;
            user.Changes = userToChange.Changes;
            user.ConcurrencyStamp = userToChange.ConcurrencyStamp;
            user.EmailConfirmed = userToChange.EmailConfirmed;
            user.IsActive = userToChange.IsActive;
            user.LockoutEnabled = userToChange.LockoutEnabled;
            user.LockoutEnd = userToChange.LockoutEnd;
            if(!String.IsNullOrEmpty(user.Email)){
                user.NormalizedEmail = user.Email.ToUpper();
            }
            user.NormalizedUserName = user.UserName.ToUpper();
            user.PasswordHash = userToChange.PasswordHash;
            user.PhoneNumberConfirmed = userToChange.PhoneNumberConfirmed;
            user.SecurityStamp = userToChange.SecurityStamp;
            user.TwoFactorEnabled = userToChange.TwoFactorEnabled;

            bool succes = await _repo.EditUser(user, userToChange);

            if(succes){
                User currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                succes = await _eventLogRepo.AddEventLogChange("bruger", user.UserName, user.Id, currentUser, user, userToChange);
            }
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

            if(succes){
                User currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                succes = await _eventLogRepo.AddEventLog(EventType.Deactivated, "bruger", user.UserName, user.Id, currentUser);
            }
            return succes ? StatusCode(200) : BadRequest();
        }

        [Authorize(Policy = "User_ActivateDeactivate")]
        [HttpPost("activate/{id}", Name = "ActivateUser")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var userActivate = await _repo.GetUser(id);
            userActivate.IsActive = true;
            bool succes = await _repo.ActivateUser(userActivate);

            if(succes){
                User currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                succes = await _eventLogRepo.AddEventLog(EventType.Activated, "bruger", userActivate.UserName, userActivate.Id, currentUser);
            }
            return succes ? StatusCode(200) : BadRequest();
        }

        [Authorize(Policy = "User_Add")]
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody]User userToAdd)
        {
            // TODO MOST LIKELY OUTDATED
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

            RoleEditDto oldRoles = new RoleEditDto();
            oldRoles.RoleNames = userRoles.ToArray();
            User currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            await _eventLogRepo.AddEventLogChange("bruger", user.UserName, user.Id, currentUser, oldRoles, roleEditDto);

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
                        User currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result; 
                        await _eventLogRepo.AddEventLog(EventType.Created, "rolle", name, _roleManager.FindByNameAsync(name).Result.Id, currentUser);
                        return StatusCode(201);
                    } 
                }
            }
            
            return BadRequest();
        }

        [Authorize(Policy = "User_Add")]
        [HttpPost("AddRoleCategory")]
        public async Task<IActionResult> AddNewRoleCategory([FromBody]RoleCategoryForAddDto roleCategory)
        {   
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            List<RoleCategoryRoleRelation> rolesToAdd = new List<RoleCategoryRoleRelation>();
            foreach(RoleForGetDto r in roleCategory.UserRoles){
                rolesToAdd.Add(new RoleCategoryRoleRelation{
                    RoleId = r.Id
                });
            }

            var roleCategoryToCreate = new RoleCategory(
                roleCategory.Name,
                rolesToAdd
            );

            bool success = await _repo.AddRoleCategory(roleCategoryToCreate);

            if(success){
                User currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                success = await _eventLogRepo.AddEventLog(EventType.Created, "Rolle", roleCategoryToCreate.Name, roleCategoryToCreate.Id, currentUser);
            }

            return success ? StatusCode(201) : BadRequest();
        }

        [Authorize(Policy = "User_View")]      
        [HttpGet("GetAllRoleCategories")]
        public async Task<IActionResult> GetRoleCategories(){
            var roles = await _repo.GetRoleCategories();

            return Ok(roles);
        }

        [Authorize(Policy = "User_View")]      
        [HttpGet("GetAllRoles")]
        public async Task<IOrderedEnumerable<Role>> GetUserRoles() {
            var roles = await _roleManager.Roles.ToListAsync();
            var sortedRoles =  roles.OrderBy(x => x.Name);
            return sortedRoles;
        }



    }
}