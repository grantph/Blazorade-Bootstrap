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
			if (Display == Display.Block)
			{
				// <div>
				builder.OpenElement(0, "div");
				builder.AddAttribute(1, "class", "form-check");
			}
			else
			{
				// <label>
				builder.OpenElement(2, "label");

				// Size
				switch (Size)
				{
					case InputSize.Large: builder.AddAttribute(3, "class", "checkbox-inline label-lg"); break;
					case InputSize.Small: builder.AddAttribute(4, "class", "checkbox-inline label-sm"); break;
					default: builder.AddAttribute(5, "class", "checkbox-inline"); break;
				}

				// Propagation
				if (StopPropagation)
				{
					builder.AddAttribute(6, "onclick", "event.stopPropagation()");
				}

				// Label Width
				if (LabelWidth.HasValue)
				{
					builder.AddAttribute(7, "style", $"width:{LabelWidth}");
				}
			}

			// <input>
			builder.OpenElement(10, "input");
			builder.AddMultipleAttributes(11, Attributes);
			builder.AddAttribute(12, "type", "checkbox");
			builder.AddAttribute(13, "checked", BindConverter.FormatValue(CurrentValue));
			builder.AddAttribute(14, "onchange", EventCallback.Factory.CreateBinder<bool>(this, __value => CurrentValue = __value, CurrentValue));

			// Disabled?
			if (Disabled ?? false)
			{
				builder.AddAttribute(15, "disabled", string.Empty);
			}

			// </input>
			builder.CloseElement();

			if (Display == Display.Block)
			{
				// <label>
				builder.OpenElement(20, "label");

				// Size
				switch (Size)
				{
					case InputSize.Large: builder.AddAttribute(21, "class", "form-check-label label-lg"); break;
					case InputSize.Small: builder.AddAttribute(22, "class", "form-check-label label-sm"); break;
					default: builder.AddAttribute(23, "class", "form-check-label"); break;
				}

				builder.AddAttribute(24, "for", Id);

				// Propagation
				if (StopPropagation)
				{
					builder.AddAttribute(25, "onclick", "event.stopPropagation()");
				}

				// Label Width
				if (LabelWidth.HasValue)
				{
					builder.AddAttribute(26, "style", $"width:{LabelWidth}");
				}

				// Content (inside Label)
				builder.AddContent(27, ChildContent);

				// </label>
				builder.CloseElement();

				// </div>
				builder.CloseElement();
			}
			else
			{
				// Content (inside Label)
				builder.AddContent(30, ChildContent);

				// </label>
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