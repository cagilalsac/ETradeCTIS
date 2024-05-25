#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Category : Record
    {
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
