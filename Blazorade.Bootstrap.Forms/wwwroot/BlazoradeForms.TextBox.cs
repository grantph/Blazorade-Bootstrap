using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazorade.Bootstrap.Forms
{
	internal static partial class BlazoradeForms
	{
		internal static ValueTask<string> TextBox_Init(this IJSRuntime target, ElementReference elementReference, IDisposable objectReference)
		{
			return target.InvokeAsync<string>("blazoradeForms.TextBox.init", elementReference, objectReference);
		}
	}
}