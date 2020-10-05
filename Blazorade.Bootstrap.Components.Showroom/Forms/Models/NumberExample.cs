using System.ComponentModel.DataAnnotations;

using Blazorade.Bootstrap.Forms;

namespace Blazorade.Bootstrap.Components.Showroom.Forms.Models
{
	public class NumberExample : IModelState
	{
		public ModelState ModelState { get; set; } = ModelState.Edit;

		public int Example1 { get; set; } = 10;

		[Required(ErrorMessage = "Example 2 is required")]
		public int Example2 { get; set; } = 20;

		public int Example3 { get; set; } = 40;

		public int Example4 { get; set; } = 50;

		[Required]
		public int? Example5 { get; set; } = null;

		[Required]
		public double Example6 { get; set; } = 1.3;

		[Required]
		public double? Example7 { get; set; } = null;
	}
}