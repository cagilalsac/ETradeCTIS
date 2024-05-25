#nullable disable

using DataAccess.Records.Bases;

namespace DataAccess.Entities
{
    public class ProductStore : Record
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }
    }
}
