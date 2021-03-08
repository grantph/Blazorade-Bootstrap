using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	public partial class FormInputTextBox
	{
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			// Label
			BuildRenderTreeLabel(builder);

			// InputGroup (Open)
			BuildRenderTreeInputGroupOpen(builder);

			switch (Mode)
			{
				case TextBoxMode.SingleLine:
					{
						// <input>
						builder.OpenElement(0, Html.INPUT);
						builder.AddMultipleAttributes(1, this.Attributes);
						builder.AddAttribute(2, Html.CLASS, CssClass); // Overwrite Attributes[Html.CLASS]
						builder.AddAttribute(3, Html.VALUE, BindConverter.FormatValue(CurrentValue));

						// Disabled?
						if (Disabled ?? false) builder.AddAttribute(4, Html.DISABLED, string.Empty);

						// Help
						if (HasHelp) builder.AddAttribute(5, Html.ARIA_DESCRIBEDBY, $"{Id}-help");

						// OnChange
						builder.AddAttribute(6, Html.ONCHANGE, EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

						// Clear or Select OnClick
						if (ClearOnClick)
						{
							builder.AddAttribute(7, Html.ONCLICK, EventCallback.Factory.Create(this, __value => CurrentValueAsString = string.Empty));
						}
						else if (SelectOnClick)
						{
							builder.AddAttribute(7, Html.ONCLICK, "this.select()");
						}

						// OnBlur
						if (OnBlur.HasDelegate)
						{
							builder.AddAttribute(9, Html.ONBLUR, EventCallback.Factory.Create(this, this.OnBlurAsync));
						}

						// OnFocus
						if (OnFocus.HasDelegate)
						{
							builder.AddAttribute(10, Html.ONFOCUS, EventCallback.Factory.Create(this, this.OnFocusAsync));
						}

						// Create an ElementReference suitable for use in JSInterop
						builder.AddElementReferenceCapture(11, (__value) => inputElement = __value);

						// </input>
						builder.CloseElement();
					}
					break;

				case TextBoxMode.MultiLine:
					{
						// <textarea>
						builder.OpenElement(100, Html.TEXTAREA);
						builder.AddMultipleAttributes(101, this.Attributes);
						builder.AddAttribute(102, Html.CLASS, CssClass); // Overwrite Attributes[Html.CLASS]
						builder.AddAttribute(103, Html.VALUE, BindConverter.FormatValue(CurrentValue));

						// Disabled?
						if (Disabled ?? false) builder.AddAttribute(104, Html.DISABLED, string.Empty);

						// Help
						if (HasHelp) builder.AddAttribute(105, Html.ARIA_DESCRIBEDBY, $"{Id}-help");

						// OnChange
						builder.AddAttribute(106, Html.ONCHANGE, EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

						// Clear or Select OnClick
						if (ClearOnClick)
						{
							builder.AddAttribute(107, Html.ONCLICK, EventCallback.Factory.Create(this, __value => CurrentValueAsString = string.Empty));
						}
						else if (SelectOnClick)
						{
							builder.AddAttribute(107, Html.ONCLICK, "this.select()");
						}

						// OnBlur
						if (OnBlur.HasDelegate)
						{
							builder.AddAttribute(109, Html.ONBLUR, EventCallback.Factory.Create(this, this.OnBlurAsync));
						}

						// OnFocus
						if (OnFocus.HasDelegate)
						{
							builder.AddAttribute(110, Html.ONFOCUS, EventCallback.Factory.Create(this, this.OnFocusAsync));
						}

						// Create an ElementReference suitable for use in JSInterop
						builder.AddElementReferenceCapture(111, (__value) => inputElement = __value);

						// </textarea>
						builder.CloseElement();
					}
					break;

				default: throw new NotImplementedException($"TextBoxMode.{Mode} not implemented in BuildRenderTree()");
			}

			// InputGroup (CLose)
			BuildRenderTreeInputGroupClose(builder);

			// Help
			BuildRenderTreeHelp(builder);
		}
	}
}