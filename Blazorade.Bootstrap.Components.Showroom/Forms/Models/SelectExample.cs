using System.ComponentModel.DataAnnotations;

using Blazorade.Bootstrap.Forms;

namespace Blazorade.Bootstrap.Components.Showroom.Forms.Models
{
	public class SelectExample : IModelState
	{
		public ModelState ModelState { get; set; } = ModelState.Edit;

		public string Example1 { get; set; } = "Anything you want...";

		[Required(ErrorMessage = "Example 2 is required")]
		public string Example2 { get; set; } = string.Empty;

		[MinLength(10, ErrorMessage = "Example 3 requires at least 10 characters")]
		public string Example3 { get; set; } = "1234567";

		[MaxLength(5, ErrorMessage = "Example 4 max 4 characters")]
		public string Example4 { get; set; } = "1234567890";

		[Required]
		public int? Example5 { get; set; } = null;

		[Required]
		public double Example6 { get; set; } = 1.3;
	}
}