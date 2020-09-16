using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	public partial class FormInputTextBox
	{
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			int seq = 0;

			// Label
			BuildRenderTreeLabel(builder);

			// InputGroup (Open)
			BuildRenderTreeInputGroupOpen(builder);

			switch (Mode)
			{
				case TextBoxMode.SingleLine:
					{
						// Input
						builder.OpenElement(seq++, "input");
						builder.AddMultipleAttributes(seq++, this.Attributes);
						builder.AddAttribute(seq++, "class", CssClass); // Overwrite Attributes["class"]
						builder.AddAttribute(seq++, "value", BindConverter.FormatValue(CurrentValue));

						// Disabled?
						if (Disabled ?? false) builder.AddAttribute(seq++, "disabled", string.Empty);

						// Help
						if (HasHelp) builder.AddAttribute(seq++, "aria-describedby", $"{Id}-help");

						// OnChange
						builder.AddAttribute(seq++, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

						// OnClick
						if (ClearOnClick) builder.AddAttribute(seq++, "onclick", $"return textBox.clearValue('{Id}')");
						else if (SelectOnClick) builder.AddAttribute(seq++, "onclick", $"return textBox.selectValue('{Id}')");

						// OnBlur
						if (OnBlur.HasDelegate) builder.AddAttribute(seq++, "onblur", EventCallback.Factory.Create(this, this.OnBlurAsync));

						builder.CloseElement();
					}
					break;

				case TextBoxMode.MultiLine:
					{
						// Input
						builder.OpenElement(seq++, "textarea");
						builder.AddMultipleAttributes(seq++, this.Attributes);
						builder.AddAttribute(seq++, "class", CssClass); // Overwrite Attributes["class"]
						builder.AddAttribute(seq++, "value", BindConverter.FormatValue(CurrentValue));

						// Disabled?
						if (Disabled ?? false) builder.AddAttribute(seq++, "disabled", string.Empty);

						// Help
						if (HasHelp) builder.AddAttribute(seq++, "aria-describedby", $"{Id}-help");

						// OnChange
						builder.AddAttribute(seq++, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

						// OnClick
						if (ClearOnClick) builder.AddAttribute(seq++, "onclick", $"return textBox.clearValue('{Id}')");
						else if (SelectOnClick) builder.AddAttribute(seq++, "onclick", $"return textBox.selectValue('{Id}')");

						// OnBlur
						if (OnBlur.HasDelegate) builder.AddAttribute(seq++, "onblur", EventCallback.Factory.Create(this, this.OnBlurAsync));

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