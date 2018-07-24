using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class UserForRegisterDto
    {
        public UserForRegisterDto(string Username, string Password){
            this.Username = Username;
            this.Password = Password;
        }
        [Required]
        public string Username { get; set; }   

        [Required]
        [StringLength(8, MinimumLength=4, ErrorMessage = "You must specify a password between 4 and 8 characters.")]
        public string Password { get; set; }
    }
}