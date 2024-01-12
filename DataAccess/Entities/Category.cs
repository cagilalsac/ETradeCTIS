#nullable disable

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
