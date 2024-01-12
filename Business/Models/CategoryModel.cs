#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class CategoryModel
    {
        #region Entity Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(75, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }
        #endregion

        #region Output Properties
        [DisplayName("Product Count")]
        public int ProductCountOutput { get; set; }

        [DisplayName("Products")]
        public string ProductsOutput { get; set; }
        #endregion
    }
}
