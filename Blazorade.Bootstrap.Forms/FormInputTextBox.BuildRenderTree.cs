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
						builder.OpenElement(0, "input");
						builder.AddMultipleAttributes(1, this.Attributes);
						builder.AddAttribute(2, "class", CssClass); // Overwrite Attributes["class"]
						builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));

						// Disabled?
						if (Disabled ?? false) builder.AddAttribute(4, "disabled", string.Empty);

						// Help
						if (HasHelp) builder.AddAttribute(5, "aria-describedby", $"{Id}-help");

						// OnChange
						builder.AddAttribute(6, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

						// OnClick
						if (ClearOnClick) builder.AddAttribute(7, "onclick", $"return textBox.clearValue('{Id}')");
						else if (SelectOnClick) builder.AddAttribute(8, "onclick", $"return textBox.selectValue('{Id}')");

						// OnBlur
						if (OnBlur.HasDelegate) builder.AddAttribute(9, "onblur", EventCallback.Factory.Create(this, this.OnBlurAsync));

						builder.CloseElement();
					}
					break;

				case TextBoxMode.MultiLine:
					{
						// Input
						builder.OpenElement(10, "textarea");
						builder.AddMultipleAttributes(11, this.Attributes);
						builder.AddAttribute(12, "class", CssClass); // Overwrite Attributes["class"]
						builder.AddAttribute(13, "value", BindConverter.FormatValue(CurrentValue));

						// Disabled?
						if (Disabled ?? false) builder.AddAttribute(14, "disabled", string.Empty);

						// Help
						if (HasHelp) builder.AddAttribute(15, "aria-describedby", $"{Id}-help");

						// OnChange
						builder.AddAttribute(16, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

						// OnClick
						if (ClearOnClick) builder.AddAttribute(17, "onclick", $"return textBox.clearValue('{Id}')");
						else if (SelectOnClick) builder.AddAttribute(18, "onclick", $"return textBox.selectValue('{Id}')");

						// OnBlur
						if (OnBlur.HasDelegate) builder.AddAttribute(19, "onblur", EventCallback.Factory.Create(this, this.OnBlurAsync));

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