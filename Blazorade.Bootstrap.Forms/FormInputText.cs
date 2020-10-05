// Copy of https://github.com/dotnet/aspnetcore/blob/8c02467b4a218df3b1b0a69bceb50f5b64f482b1/src/Components/Web/src/Forms/InputText.cs
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	/* This is almost equivalent to a .razor file containing:
     *
     *    @inherits InputBase<string>
     *    <input @bind="CurrentValue" id="@Id" class="@CssClass" />
     *
     * The only reason it's not implemented as a .razor file is that we don't presently have the ability to compile those
     * files within this project. Developers building their own input components should use Razor syntax.
     */

	/// <summary>
	/// An input component for editing <see cref="string"/> values.
	/// </summary>
	public class FormInputText : FormInputBase<string>
	{
		public FormInputText() : base()
		{
			// This is a Bootstrap 4 Form Control
			base.BootstrapFormControl = true;
		}

		/// <inheritdoc />
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			builder.OpenElement(0, Html.INPUT);
			builder.AddMultipleAttributes(1, Attributes);
			builder.AddAttribute(2, Html.CLASS, CssClass); // Overwrite class in Attributes
			builder.AddAttribute(3, Html.VALUE, BindConverter.FormatValue(CurrentValue));
			builder.AddAttribute(4, Html.ONCHANGE, EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

			// Disabled?
			if (Disabled ?? false)
			{
				builder.AddAttribute(5, Html.DISABLED, string.Empty);
			}

			// Help
			if (HasHelp)
			{
				builder.AddAttribute(6, Html.ARIA_DESCRIBEDBY, $"{Id}-help");
			}

			builder.CloseElement();

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