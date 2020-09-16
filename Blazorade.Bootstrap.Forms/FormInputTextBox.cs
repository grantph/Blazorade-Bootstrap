using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazorade.Bootstrap.Forms
{
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
			await this.OnBlur.InvokeAsync(this);
		}

		private async Task Input_OnClick()
		{
			if (ClearOnClick)
			{
				// Update Client-Side
				await JSRuntime.InvokeAsync<bool>("blazoradeForms.textBox.clearValue", new object[] { Id });

				// Update Server-Side
				this.Value = string.Empty;
			}
			else if (SelectOnClick)
			{
				// Update Client-Side
				await JSRuntime.InvokeAsync<bool>("blazoradeForms.textBox.selectValue", new object[] { Id });
			}
		}

		private async Task Input_OnChange(ChangeEventArgs e)
		{
			// Update Client-Side
			await JSRuntime.InvokeAsync<bool>("blazoradeForms.textBox.updateValue", new object[] { Id, e.Value });

			// Server-Side Event
			if (this.OnChange.HasDelegate) await this.OnChange.InvokeAsync(e);
		}

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
						AddAttribute("type", Type.ToString().ToLower());
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
			else if (MaxLength.HasValue == false)
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
	}
}