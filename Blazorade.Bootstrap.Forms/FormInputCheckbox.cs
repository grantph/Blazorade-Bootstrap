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
				builder.OpenElement(0, Html.DIV);
				builder.AddAttribute(1, Html.CLASS, Bootstrap.FORM_CHECK);
			}
			else
			{
				// <label>
				builder.OpenElement(2, Html.LABEL);

				// Size
				switch (Size)
				{
					case InputSize.Large: builder.AddAttribute(3, Html.CLASS, $"{Bootstrap.CHECKBOX_INLINE} {Bootstrap.LABEL_LARGE}"); break;
					case InputSize.Small: builder.AddAttribute(4, Html.CLASS, $"{Bootstrap.CHECKBOX_INLINE} {Bootstrap.LABEL_SMALL}"); break;

					default: builder.AddAttribute(5, Html.CLASS, Bootstrap.CHECKBOX_INLINE); break;
				}

				// Propagation
				if (StopPropagation)
				{
					builder.AddAttribute(6, Html.ONCLICK, "event.stopPropagation()");
				}

				// Label Width
				if (LabelWidth.HasValue)
				{
					builder.AddAttribute(7, Html.STYLE, $"width:{LabelWidth}");
				}
			}

			// <input>
			builder.OpenElement(10, Html.INPUT);
			builder.AddMultipleAttributes(11, Attributes);

			// type=checkbox
			builder.AddAttribute(12, Html.TYPE, Html.CHECKBOX);

			// Help
			if (HasHelp)
			{
				builder.AddAttribute(13, Html.ARIA_DESCRIBEDBY, $"{Id}-{Bootstrap.HELP}");
			}

			// Checked
			builder.AddAttribute(14, Html.CHECKED, BindConverter.FormatValue(CurrentValue));

			// OnChange
			builder.AddAttribute(15, Html.ONCHANGE, EventCallback.Factory.CreateBinder<bool>(this, __value => CurrentValue = __value, CurrentValue));

			// Disabled?
			if (Disabled ?? false)
			{
				builder.AddAttribute(16, Html.DISABLED, string.Empty);
			}

			// </input>
			builder.CloseElement();

			if (Display == Display.Block)
			{
				// BLOCK

				// <label>
				builder.OpenElement(20, Html.LABEL);

				// Size
				switch (Size)
				{
					case InputSize.Large:

						builder.AddAttribute(21, Html.CLASS, $"{Bootstrap.FORM_CHECK_LABEL} {Bootstrap.LABEL_LARGE}");

						break;

					case InputSize.Small:

						builder.AddAttribute(22, Html.CLASS, $"{Bootstrap.FORM_CHECK_LABEL} {Bootstrap.LABEL_SMALL}");

						break;

					default:

						builder.AddAttribute(23, Html.CLASS, Bootstrap.FORM_CHECK_LABEL);

						break;
				}

				builder.AddAttribute(24, Html.FOR, Id);

				// Propagation
				if (StopPropagation)
				{
					builder.AddAttribute(25, Html.ONCLICK, "event.stopPropagation()");
				}

				// Label Width
				if (LabelWidth.HasValue)
				{
					builder.AddAttribute(26, Html.STYLE, $"width:{LabelWidth}");
				}

				// Label (preferred) or Content
				if (!string.IsNullOrEmpty(Label))
				{
					builder.AddContent(30, Label);
				}
				else
				{
					builder.AddContent(30, ChildContent);
				}

				// </label>
				builder.CloseElement();

				// </div>
				builder.CloseElement();
			}
			else
			{
				// INLINE

				// Label (preferred) or Content
				if (!string.IsNullOrEmpty(Label))
				{
					builder.AddContent(30, Label);
				}
				else
				{
					builder.AddContent(30, ChildContent);
				}

				// </label>
				builder.CloseElement();
			}

			// Help
			BuildRenderTreeHelp(builder);
		}

		/// <inheritdoc />
		protected override bool TryParseValueFromString(string value, out bool result, out string validationErrorMessage)
			=> throw new NotImplementedException($"This component does not parse string inputs. Bind to the '{nameof(CurrentValue)}' property, not '{nameof(CurrentValueAsString)}'.");

		protected override void OnParametersSet()
		{
			// Require Id
			SetIdIfEmpty();

			// Form Checkbox
			AddClasses(Bootstrap.FORM_CHECK_INPUT);

			base.OnParametersSet();
		}
	}
}