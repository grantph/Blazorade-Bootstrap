using System.ComponentModel.DataAnnotations;

using Blazorade.Bootstrap.Forms;

namespace Blazorade.Bootstrap.Components.Showroom.Forms.Models
{
	public class CheckboxExample : IModelState
	{
		public ModelState ModelState { get; set; } = ModelState.Edit;

		public bool Example1 { get; set; } = false;

		[Required(ErrorMessage = "Example 2 is required")]
		public bool Example2 { get; set; } = true;

		public bool Example3 { get; set; } = false;

		public bool Example4 { get; set; } = true;

		public bool Example5 { get; set; } = false;

		[Required(ErrorMessage = "Example 6 is required")]
		public bool Example6 { get; set; } = true;
	}
}