using Microsoft.AspNetCore.Components;

namespace Blazorade.Bootstrap.Forms
{
	public partial class Option
	{
		[Parameter] public string Name { get; set; }
		[Parameter] public string Value { get; set; }
		[Parameter] public bool Selected { get; set; }

		protected override void OnParametersSet()
		{
			if (!string.IsNullOrEmpty(Value))
			{
				AddAttribute(Html.VALUE, Value);
			}

			if (Selected)
			{
				AddAttribute("selected", string.Empty);
			}

			base.OnParametersSet();
		}
	}
}