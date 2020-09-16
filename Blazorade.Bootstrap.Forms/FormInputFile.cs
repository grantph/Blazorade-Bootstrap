using System;
using System.IO;
using System.Threading.Tasks;

using Blazorade.Bootstrap.Forms.File;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace Blazorade.Bootstrap.Forms
{
	/// <summary>
	///   <para>
	/// Hybrid implementation of <see cref="https://github.com/SteveSandersonMS/BlazorInputFile">BlazorInputFile</see> and
	/// <see cref="https://github.com/Blazorade/Blazorade-Bootstrap">Blazorade</see>. All rendering of BlazorFileInput has
	/// been converted to code behind. Pay particular attention to AddElementReferenceCapture which shows the correct way to create
	/// a reference between DOM and Blazor that can then be used in Javascript Interop. This took some experimentation as it's poorly
	/// documented, however, extremely useful when passing an ElementReference to Javascript.</para>
	///   <para>
	/// Unfortunately, @bind-Value is required as InputBase expects it. Currently using a Dummy string property to overcome this
	/// limitation @bind-Value="Dummy" but would like to develop a more permanent solution for overriding default Binding behavior.</para>
	/// </summary>
	/// <seealso cref="https://getbootstrap.com/docs/4.1/components/input-group/#custom-file-input" />
	public class FormInputFile : FormInputBase<int>, IDisposable
	{
		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		[Parameter] public int MaxMessageSize { get; set; } = 20 * 1024; // TODO: Use SignalR default
		[Parameter] public int MaxBufferSize { get; set; } = 1024 * 1024;

		[Parameter] public EventCallback<IFileListEntry[]> OnChange { get; set; }

		public FormInputFile() : base()
		{
			// Dummy ValueExpression and ValueChanged to override FormInputBase
			//ValueExpression = () => string.Empty;
			//ValueChanged = EventCallback.Factory.Create<string>(this, () => { });
		}

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			int seq = 0;

			// Open Input Group
			BuildRenderTreeInputGroupOpen(builder);

			// Prepend
			BuildRenderTreePrepend(builder);

			// File Input. Indenting to make the <div></div> block clear.
			{
				// Div
				builder.OpenElement(seq++, "div");
				builder.AddAttribute(seq++, "class", "custom-file");

				// Input
				// < input type = "file" @ref = "inputFileElement" @attributes = "UnmatchedParameters" />
				builder.OpenElement(seq++, "input");
				builder.AddMultipleAttributes(seq++, Attributes);
				builder.AddAttribute(seq++, "type", "file");

				//builder.AddAttribute(seq++, "onchange", EventCallback.Factory.Create<ChangeEventArgs>(this, NotifyChange));

				// Create an ElementReference suitable for use in JSInterop
				builder.AddElementReferenceCapture(seq++, (__value) => inputFileElement = __value);

				builder.CloseElement();

				// Label
				builder.OpenElement(seq++, "label");
				builder.AddAttribute(seq++, "class", ScreenReaderOnly ? "sr-only custom-file-label" : "custom-file-label");
				builder.AddAttribute(seq++, "for", $"{Id}");
				builder.AddContent(seq++, Label);
				builder.CloseElement();

				// /Div
				builder.CloseElement();
			}

			// Append
			BuildRenderTreeAppend(builder);

			// Close Input Group
			BuildRenderTreeInputGroupClose(builder);

			// Help
			BuildRenderTreeHelp(builder);

			base.BuildRenderTree(builder);
		}

		private ElementReference inputFileElement;
		private IDisposable thisReference;
		private bool disposedValue;

		[JSInvokable]
		public Task NotifyChange(FileListEntryImpl[] files)
		{
			foreach (var file in files)
			{
				// So that method invocations on the file can be dispatched back here
				file.Owner = (FormInputFile)(object)this;
			}

			return OnChange.InvokeAsync(files);
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				thisReference = DotNetObjectReference.Create(this);
				await JSRuntime.InvokeAsync<object>("BlazorInputFile.init", inputFileElement, thisReference);
			}
		}

		internal Stream OpenFileStream(FileListEntryImpl file)
		{
			return SharedMemoryFileListEntryStream.IsSupported(JSRuntime)
				? (Stream)new SharedMemoryFileListEntryStream(JSRuntime, inputFileElement, file)
				: new RemoteFileListEntryStream(JSRuntime, inputFileElement, file, MaxMessageSize, MaxBufferSize);
		}

		internal async Task<FileListEntryImpl> ConvertToImageFileAsync(FileListEntryImpl file, string format, int maxWidth, int maxHeight)
		{
			FileListEntryImpl imageFile = await JSRuntime.InvokeAsync<FileListEntryImpl>("BlazorInputFile.toImageFile", inputFileElement, file.Id, format, maxWidth, maxHeight);

			// So that method invocations on the file can be dispatched back here
			imageFile.Owner = (FormInputFile)(object)this;

			return imageFile;
		}

		protected override void OnParametersSet()
		{
			// Required Id?
			if (HasLabel || HasInputGroup) SetIdIfEmpty();

			// Form Control
			AddClasses("form-control-file");

			// Prepend Reference
			if (HasPrepend) AddAttribute("aria-describedby", $"{Id}-prepend");

			base.OnParametersSet();
		}

		/// <summary>
		/// Always returns <see langword="true"/>.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="result"></param>
		/// <param name="validationErrorMessage"></param>
		/// <returns></returns>
		protected override bool TryParseValueFromString(string value, out int result, out string validationErrorMessage)
		{
			result = 0;
			validationErrorMessage = null;

			return true;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// Dispose managed state (managed objects)
					thisReference?.Dispose();
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}