#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class User : Record
    {
        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
