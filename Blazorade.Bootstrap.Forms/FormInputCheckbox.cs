// Copy of https://github.com/dotnet/aspnetcore/blob/8c02467b4a218df3b1b0a69bceb50f5b64f482b1/src/Components/Web/src/Forms/InputCheckbox.cs
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

using Blazorade.Bootstrap.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	/* This is exactly equivalent to a .razor file containing:
     *
     *    @inherits InputBase<bool>
     *    <input type="checkbox" @bind="CurrentValue" id="@Id" class="@CssClass" />
     *
     * The only reason it's not implemented as a .razor file is that we don't presently have the ability to compile those
     * files within this project. Developers building their own input components should use Razor syntax.
     */

	/// <summary>
	/// An input component for editing <see cref="bool"/> values.
	/// </summary>
	public class FormInputCheckbox : FormInputBase<bool>
	{
		/// <summary>
		/// Checkbox display mode. Defaults to Inline.
		/// </summary>
		[Parameter] public Display Display { get; set; } = Display.Inline;

		/// <summary>
		/// Label width.
		/// </summary>
		[Parameter] public Size? LabelWidth { get; set; }

		/// <summary>
		/// Stop the click event bubbling to the up.
		/// </summary>
		[Parameter] public bool StopPropagation { get; set; } = false;

		/// <inheritdoc />
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			int seq = 0;

			if (Display == Display.Block)
			{
				// Div
				builder.OpenElement(seq++, "div");
				builder.AddAttribute(seq++, "class", "form-check");
			}
			else
			{
				// Label
				builder.OpenElement(seq++, "label");

				// Size
				switch (Size)
				{
					case InputSize.Large: builder.AddAttribute(seq++, "class", "checkbox-inline label-lg"); break;
					case InputSize.Small: builder.AddAttribute(seq++, "class", "checkbox-inline label-sm"); break;
					default: builder.AddAttribute(seq++, "class", "checkbox-inline"); break;
				}

				// Propagation
				if (StopPropagation) builder.AddAttribute(seq++, "onclick", "event.stopPropagation()");

				// Label Width
				if (LabelWidth.HasValue) builder.AddAttribute(seq++, "style", $"width:{LabelWidth}");
			}

			{
				// Input
				builder.OpenElement(seq++, "input");
				builder.AddMultipleAttributes(seq++, Attributes);
				builder.AddAttribute(seq++, "type", "checkbox");
				builder.AddAttribute(seq++, "checked", BindConverter.FormatValue(CurrentValue));
				builder.AddAttribute(seq++, "onchange", EventCallback.Factory.CreateBinder<bool>(this, __value => CurrentValue = __value, CurrentValue));

				// Disabled?
				if (Disabled ?? false) builder.AddAttribute(seq++, "disabled", string.Empty);

				builder.CloseElement();
			}

			if (Display == Display.Block)
			{
				// Label
				builder.OpenElement(seq++, "label");

				// Size
				switch (Size)
				{
					case InputSize.Large: builder.AddAttribute(seq++, "class", "form-check-label label-lg"); break;
					case InputSize.Small: builder.AddAttribute(seq++, "class", "form-check-label label-sm"); break;
					default: builder.AddAttribute(seq++, "class", "form-check-label"); break;
				}

				builder.AddAttribute(seq++, "for", Id);

				// Propagation
				if (StopPropagation) builder.AddAttribute(seq++, "onclick", "event.stopPropagation()");

				// Label Width
				if (LabelWidth.HasValue) builder.AddAttribute(seq++, "style", $"width:{LabelWidth}");

				// Content (inside Label)
				builder.AddContent(seq++, ChildContent);

				// Label
				builder.CloseElement();

				// Div
				builder.CloseElement();
			}
			else
			{
				// Content (inside Label)
				builder.AddContent(seq++, ChildContent);

				// Label
				builder.CloseElement();
			}
		}

		/// <inheritdoc />
		protected override bool TryParseValueFromString(string value, out bool result, out string validationErrorMessage)
			=> throw new NotImplementedException($"This component does not parse string inputs. Bind to the '{nameof(CurrentValue)}' property, not '{nameof(CurrentValueAsString)}'.");

		protected override void OnParametersSet()
		{
			// Require Id
			SetIdIfEmpty();

			// Form Checkbox
			AddClasses("form-check-input");

			base.OnParametersSet();
		}
	}
}