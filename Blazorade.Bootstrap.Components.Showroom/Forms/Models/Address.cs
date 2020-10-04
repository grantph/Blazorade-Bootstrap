using Blazorade.Bootstrap.Forms;

namespace Blazorade.Bootstrap.Components.Showroom.Forms.Models
{
	internal interface IAddress : IModelState
	{
		string Street { get; set; }
	}

	public class Address : IAddress
	{
		public ModelState ModelState { get; set; } = ModelState.Edit;

		public string Street { get; set; }
	}
}