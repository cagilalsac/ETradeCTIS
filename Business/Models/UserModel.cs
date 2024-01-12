#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserModel
    {
        #region Entity Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int? RoleId { get; set; }
        #endregion

        #region Output Properties
        public string RoleOutput { get; set; }
        #endregion
    }
}
