using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace Blazorade.Bootstrap.Forms
{
	public partial class FormGroup
	{
		[Parameter] public string Label { get; set; } = string.Empty;

		/// <summary>
		/// Bootstrap Input group that adds a help block to the input control.
		/// </summary>
		[Parameter] public string Help { get; set; }

		/// <summary>
		/// Bootstrap Input group help mode. Defaults to Block.
		/// </summary>
		[Parameter] public Display HelpDisplay { get; set; } = Display.Block;

		protected bool HasHelp => !string.IsNullOrEmpty(Help);

		public FormGroup()
		{
		}

		// Debugging.
		//private static int count = 0;

		protected override async Task OnParametersSetAsync()
		{
			// Debugging. Sometimes it's nice to know which FormGroup we are in when debugging errors. It might be nice to build this in somehow, as it can help save debug time.
			//Console.WriteLine($"{nameof(FormGroup)} {count++}");

			// Add FormGroup Class
			AddClasses(Bootstrap.FORM_GROUP);

			await base.OnParametersSetAsync();
		}
	}
}