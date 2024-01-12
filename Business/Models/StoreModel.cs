#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class StoreModel
	{
		#region Entity Properties
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} is required!")]
		[StringLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
		public string Name { get; set; }
		#endregion
	}
}
