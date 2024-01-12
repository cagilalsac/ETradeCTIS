#nullable disable

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<ProductStore> ProductStores { get; set; }
    }
}
