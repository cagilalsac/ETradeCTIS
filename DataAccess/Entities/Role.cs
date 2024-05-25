#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Role : Record
    {
        [Required]
        [StringLength(5)]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
