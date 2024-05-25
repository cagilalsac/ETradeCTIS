#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Store : Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<ProductStore> ProductStores { get; set; }
    }
}
