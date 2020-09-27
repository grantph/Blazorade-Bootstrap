// https://github.com/dotnet/aspnetcore/blob/8c02467b4a218df3b1b0a69bceb50f5b64f482b1/src/Components/Web/src/Forms/InputTextArea.cs
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	/* This is almost equivalent to a .razor file containing:
     *
     *    @inherits InputBase<string>
     *    <textarea @bind="CurrentValue" id="@Id" class="@CssClass"></textarea>
     *
     * The only reason it's not implemented as a .razor file is that we don't presently have the ability to compile those
     * files within this project. Developers building their own input components should use Razor syntax.
     */

	/// <summary>
	/// A multiline input component for editing <see cref="string"/> values.
	/// </summary>
	public class FormInputTextArea : FormInputBase<string>
	{
		public FormInputTextArea() : base()
		{
			// This is a Bootstrap 4 Form Control
			base.BootstrapFormControl = true;
		}

		/// <inheritdoc />
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			{
				builder.OpenElement(0, "textarea");
				builder.AddMultipleAttributes(1, Attributes);
				builder.AddAttribute(2, "class", CssClass); // Overwrite class in Attributes
				builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
				builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

				// Disabled?
				if (Disabled ?? false)
				{
					builder.AddAttribute(5, "disabled", string.Empty);
				}

				// Help
				if (HasHelp)
				{
					builder.AddAttribute(6, "aria-describedby", $"{Id}-help");
				}

				builder.CloseElement();
			}

			// Help
			BuildRenderTreeHelp(builder);
		}

		/// <inheritdoc />
		protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
		{
			result = value;
			validationErrorMessage = null;
			return true;
		}
	}
}