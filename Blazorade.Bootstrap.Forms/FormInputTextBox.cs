using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazorade.Bootstrap.Forms
{
	public class PasteEventArgs : EventArgs
	{
		public string PasteText { get; set; } = string.Empty;
		public bool Cancel { get; set; } = false;
	}

	/// <summary>
	/// Combines input type=text and textarea into a single control.
	/// </summary>
	public partial class FormInputTextBox : FormInputBase<string>
	{
		public FormInputTextBox() : base()
		{
			base.BootstrapFormControl = true;
		}

		[Inject]
		private IJSRuntime JSRuntime { get; set; }

		[Parameter]
		public TextBoxType Type { get; set; } = TextBoxType.Text;

		/// <summary>
		/// SingleLine or MultiLine mode.
		/// </summary>
		[Parameter]
		public TextBoxMode Mode { get; set; } = TextBoxMode.SingleLine;

		[Parameter]
		public int Rows { get; set; } = 1;

		/// <summary>
		/// Override the max character length of the TextBox. Rendered as maxlength='value'. If Null, then the MaxLengthAttribute will be used. Defaults to Null.
		/// </summary>
		[Parameter]
		public int? MaxLength { get; set; } = null;

		/// <summary>
		/// Called by Javascript when the 'paste' event is fired.
		/// </summary>
		/// <param name="pasteText"></param>
		/// <returns>True when default paste behavior should be prevented</returns>
		[JSInvokable]
		public async Task<bool> NotifyPasteAsync(string pasteText)
		{
			// OnPaste configured?
			if (OnPaste.HasDelegate)
			{
				// Call OnPaste
				await OnPaste.InvokeAsync(pasteText);

				return true;
			}

			return false;
		}

		[Parameter]
		public bool ClearOnClick { get; set; } = false;

		[Parameter]
		public bool SelectOnClick { get; set; } = false;

		[Parameter]
		public EventCallback<ChangeEventArgs> OnChange { get; set; }

		[Parameter]
		public EventCallback OnBlur { get; set; }

		protected virtual async Task OnBlurAsync()
		{
			if (OnBlur.HasDelegate)
			{
				await this.OnBlur.InvokeAsync(this);
			}
		}

		[Parameter]
		public EventCallback OnFocus { get; set; }

		protected virtual async Task OnFocusAsync()
		{
			if (OnFocus.HasDelegate)
			{
				await this.OnFocus.InvokeAsync(this);
			}
		}

		/// <summary>
		/// Called when the user pastes into the TextBox. Occurs immediately before the value is pasted.
		/// </summary>
		[Parameter]
		public EventCallback<string> OnPaste { get; set; }

		protected override void OnParametersSet()
		{
			// Require Id?
			if (HasLabel || HasHelp) SetIdIfEmpty();

			// Mode?
			switch (Mode)
			{
				// Single line?
				case TextBoxMode.SingleLine:
					{
						// Type
						AddAttribute(Html.TYPE, Type.ToString().ToLower());
					}
					break;

				// Multi line?
				case TextBoxMode.MultiLine:
					{
						// Rows
						AddAttribute("rows", Rows);
					}
					break;
			}

			// Has explicit MaxLength?
			if (MaxLength.HasValue && MaxLength >= 0)
			{
				AddAttribute("maxlength", MaxLength.Value.ToString());
			}
			else if (!MaxLength.HasValue)
			{
				// No explicit MaxLength. So try MaxLengthAttribute.
				var maxLengthAttribute = FieldIdentifier.Model.GetAttribute<MaxLengthAttribute>(FieldIdentifier.FieldName);

				// Found MaxLength attribute?
				if (maxLengthAttribute != null) AddAttribute("maxlength", maxLengthAttribute.Length);
			}

			// Id
			if (!string.IsNullOrEmpty(Id))
			{
				AddAttribute("id", Id);
			}

			base.OnParametersSet();
		}

		protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
		{
			// Copy Value to Result (no validation required)
			result = value;

			// Error Message
			validationErrorMessage = null;

			// Success
			return true;
		}

		private ElementReference inputElement;
		private IDisposable thisReference;
		private bool disposedValue;

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				thisReference = DotNetObjectReference.Create(this);
				await JSRuntime.TextBox_Init(inputElement, thisReference);
			}
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