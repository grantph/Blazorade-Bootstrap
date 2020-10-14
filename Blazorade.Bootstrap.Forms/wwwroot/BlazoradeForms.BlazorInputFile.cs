using System;
using System.Threading.Tasks;

using Blazorade.Bootstrap.Forms.File;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazorade.Bootstrap.Forms
{
	internal static partial class BlazoradeForms
	{
		internal static ValueTask<object> BlazorInputFile_Init(this IJSRuntime target, ElementReference elementReference, IDisposable objectReference)
		{
			return target.InvokeAsync<object>("blazoradeForms.BlazorInputFile.init", elementReference, objectReference);
		}

		internal static ValueTask<FileListEntryImpl> BlazorInputFile_ToImageFile(this IJSRuntime target, ElementReference elementReference, int id, string format, int maxWidth, int maxHeight)
		{
			return target.InvokeAsync<FileListEntryImpl>("blazoradeForms.BlazorInputFile.toImageFile", elementReference, id, format, maxWidth, maxHeight);
		}
	}
}