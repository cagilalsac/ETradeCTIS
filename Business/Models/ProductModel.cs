#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class ProductModel
    {
        #region Entity Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Unit Price")]
        public decimal? UnitPrice { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [DisplayName("Discontinued")]
        public bool IsDiscontinued { get; set; }

        [Required]
        [DisplayName("Category")]
        public int? CategoryId { get; set; }
        #endregion

        #region Output Properties
        [DisplayName("Unit Price")]
        public string UnitPriceOutput { get; set; }

        [DisplayName("Expiration Date")]
        public string ExpirationDateOutput { get; set; }

        [DisplayName("Discontinued")]
        public string IsDiscontinuedOutput { get; set; }

        [DisplayName("Category")]
        public string CategoryOutput { get; set; }

        [DisplayName("Stores")]
        public string StoresOutput { get; set; }
        #endregion

        #region Input Properties
        [DisplayName("Stores")]
        public List<int> StoreIdsInput { get; set; }
        #endregion
    }
}
