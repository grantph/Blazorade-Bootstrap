using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace Blazorade.Bootstrap.Forms
{
	public partial class FormGroup
	{
		[Parameter] public string Label { get; set; } = string.Empty;

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
			AddClasses("form-group");

			await base.OnParametersSetAsync();
		}
	}
}