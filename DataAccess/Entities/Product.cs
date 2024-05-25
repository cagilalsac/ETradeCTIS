#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Product : Record
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public bool IsDiscontinued { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<ProductStore> ProductStores { get; set; }
    }
}
