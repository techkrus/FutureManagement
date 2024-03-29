using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{   [AllowAnonymous]
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await _context.Users
                    .Where(x => x.UserName == username)
                    .Include(x => x.UserRoles)
                    .FirstOrDefaultAsync();
            if(user == null)
                return null;
            
            if(user.IsActive == false){
                return null;
            }
            return user;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++){
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public List<Role> GetRoles(ICollection<RoleCategory> roleCategories){
            var result = new List<Role>();
            foreach (var role in roleCategories){
                var relation = _context.RoleCategoryRoleRelation.Where(x => x.RoleCategoryId == role.Id);
                foreach(var r in relation) {
                    var roleToAdd = _context.Roles.FirstOrDefault(x => x.Id == r.RoleId);
                    result.Add(roleToAdd);
                }
            }
            return result;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            username = username.ToLower();
            if(await _context.Users.AnyAsync(x => x.UserName == username)){
                return true;
            }
            return false;
        }

        public async Task<List<UserRoleCategoryRelation>> GetRoleCategories(ICollection<RoleCategory> roleCategories){
            List<UserRoleCategoryRelation> userRoleCategories = new List<UserRoleCategoryRelation>();

            foreach(RoleCategory roleCategory in roleCategories){
                UserRoleCategoryRelation relation = new UserRoleCategoryRelation(){
                    RoleCategory = await _context.RoleCategories.FirstOrDefaultAsync(x => x.Id == roleCategory.Id)
                };
                userRoleCategories.Add(relation);
            }

            return userRoleCategories;
        }
    }
}