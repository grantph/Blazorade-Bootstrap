using System.ComponentModel.DataAnnotations;

using Blazorade.Bootstrap.Forms;

namespace Blazorade.Bootstrap.Components.Showroom.Forms.Models
{
	public class TextExample : IModelState
	{
		public ModelState ModelState { get; set; } = ModelState.Edit;

		public string Example1 { get; set; } = "Anything you want...";

		[Required(ErrorMessage = "Example 2 is required")]
		public string Example2 { get; set; } = string.Empty;

		[MinLength(10, ErrorMessage = "Example 3 requires at least 10 characters")]
		public string Example3 { get; set; } = "1234567";

		[MaxLength(5, ErrorMessage = "Example 4 max 4 characters")]
		public string Example4 { get; set; } = "1234567890";

		[StringLength(10, MinimumLength = 4, ErrorMessage = "Example 5 should have 4 to 10 characters")]
		public string Example5 { get; set; } = "abc";

		[MinLength(4, ErrorMessage = "Min 4 characters"), MaxLength(10), Required(ErrorMessage = "Example 6 is required")]
		public string Example6 { get; set; }

		[RegularExpression("\\d{2,4}", ErrorMessage = "Example 7 expects 2 to 4 numeric digits"), Required]
		public string Example7 { get; set; } = "NNNN";

		[Compare(nameof(Example7), ErrorMessage = "Example 8 must match Example 7")]
		public string Example8 { get; set; } = "NN";

		public string Example9 { get; set; }
		public string Example10 { get; set; }
		public string Example11 { get; set; }
		public string Example12 { get; set; }
	}
}