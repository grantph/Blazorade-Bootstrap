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
						// Input
						builder.OpenElement(0, Html.INPUT);
						builder.AddMultipleAttributes(1, this.Attributes);
						builder.AddAttribute(2, Html.CLASS, CssClass); // Overwrite Attributes["class"]
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
						if (OnBlur.HasDelegate) builder.AddAttribute(9, Html.ONBLUR, EventCallback.Factory.Create(this, this.OnBlurAsync));

						builder.CloseElement();
					}
					break;

				case TextBoxMode.MultiLine:
					{
						// Input
						builder.OpenElement(10, Html.TEXTAREA);
						builder.AddMultipleAttributes(11, this.Attributes);
						builder.AddAttribute(12, Html.CLASS, CssClass); // Overwrite Attributes["class"]
						builder.AddAttribute(13, Html.VALUE, BindConverter.FormatValue(CurrentValue));

						// Disabled?
						if (Disabled ?? false) builder.AddAttribute(14, Html.DISABLED, string.Empty);

						// Help
						if (HasHelp) builder.AddAttribute(15, Html.ARIA_DESCRIBEDBY, $"{Id}-help");

						// OnChange
						builder.AddAttribute(16, Html.ONCHANGE, EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

						// Clear or Select OnClick
						if (ClearOnClick)
						{
							builder.AddAttribute(17, Html.ONCLICK, EventCallback.Factory.Create(this, __value => CurrentValueAsString = string.Empty));
						}
						else if (SelectOnClick)
						{
							builder.AddAttribute(17, Html.ONCLICK, "this.select()");
						}

						// OnBlur
						if (OnBlur.HasDelegate)
						{
							builder.AddAttribute(19, Html.ONBLUR, EventCallback.Factory.Create(this, this.OnBlurAsync));
						}

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