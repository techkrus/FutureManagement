using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserRole
    {
        public UserRole(int id, string name)
        {
            this.Id = id;
            this.Name = name;

        }
        
        [Key]
        public int Id { get; private set; }
        public string Name { get; set; }
    }
}