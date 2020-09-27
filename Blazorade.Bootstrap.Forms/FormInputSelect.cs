// Copy of https://github.com/dotnet/aspnetcore/blob/8c02467b4a218df3b1b0a69bceb50f5b64f482b1/src/Components/Web/src/Forms/InputSelect.cs
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	/// <summary>
	/// A dropdown selection component.
	/// </summary>
	public class FormInputSelect<TValue> : FormInputBase<TValue>
	{
		public FormInputSelect() : base()
		{
			// This is a Bootstrap 4 Form Control
			base.BootstrapFormControl = true;
		}

		/// <summary>
		/// Gets or sets the error message used when displaying an a parsing error.
		/// </summary>
		[Parameter] public string ParsingErrorMessage { get; set; } = "The {0} field could not be parsed.";

		[Parameter] public string EmptyValue { get; set; } = string.Empty;
		[Parameter] public string EmptyErrorMessage { get; set; } = "{0} is empty";

		/// <inheritdoc />
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			if (HasLabel)
			{
				// <label>
				builder.OpenElement(0, "label");
				builder.AddAttribute(1, "for", Id);
				builder.AddContent(2, Label);
				builder.CloseElement();
				// </label>
			}

			if (HasInputGroup)
			{
				// <div>
				builder.OpenElement(3, "div");
				builder.AddAttribute(4, "class", "input-group");
			}

			if (HasPrepend)
			{
				// Div
				builder.OpenElement(5, "div");
				builder.AddAttribute(6, "class", "input-group-prepend");

				// Span
				builder.OpenElement(7, "span");
				builder.AddAttribute(8, "class", "input-group-text");
				builder.AddContent(9, Prepend);
				builder.CloseElement();

				builder.CloseElement();
			}

			// Select
			builder.OpenElement(10, "select");
			builder.AddMultipleAttributes(11, Attributes);
			builder.AddAttribute(12, "value", BindConverter.FormatValue(CurrentValueAsString));
			builder.AddAttribute(13, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

			// Disabled?
			if (Disabled ?? false) builder.AddAttribute(20, "disabled", string.Empty);

			// Help
			if (HasHelp) builder.AddAttribute(21, "aria-describedby", $"{Id}-help");

			// Content
			builder.AddContent(22, ChildContent);

			builder.CloseElement();

			if (HasAppend)
			{
				// Div
				builder.OpenElement(23, "div");
				builder.AddAttribute(24, "class", "input-group-append");

				// Span
				builder.OpenElement(25, "span");
				builder.AddAttribute(26, "class", "input-group-text");
				builder.AddContent(27, Append);
				builder.CloseElement();

				builder.CloseElement();
			}

			if (HasInputGroup)
			{
				builder.CloseElement();
			}

			// Help
			BuildRenderTreeHelp(builder);
		}

		/// <inheritdoc />
		protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
		{
			if (string.IsNullOrEmpty(value) || value == EmptyValue)
			{
				validationErrorMessage = string.Format(EmptyErrorMessage, Label);
				result = default;
				return false;
			}
			else if (typeof(TValue) == typeof(string))
			{
				result = (TValue)(object)value;
				validationErrorMessage = null;
				return true;
			}
			else if (typeof(TValue).IsEnum)
			{
				var success = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var parsedValue);
				if (success)
				{
					result = parsedValue;
					validationErrorMessage = null;
					return true;
				}
				else
				{
					result = default;
					validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
					return false;
				}
			}
			else if (BindConverter.TryConvertTo(value, CultureInfo.InvariantCulture, out result))
			{
				validationErrorMessage = null;
				return true;
			}
			else
			{
				validationErrorMessage = string.Format(ParsingErrorMessage, FieldIdentifier.FieldName);
				return false;
			}

			throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TValue)}'.");
		}

		/*
		/// <summary>
		/// Formats the value as a string. Derived classes can override this to determine the formating used for <c>CurrentValueAsString</c>.
		/// </summary>
		/// <param name="value">The value to format.</param>
		/// <returns>A string representation of the value.</returns>
		protected override string FormatValueAsString(TValue value)
		{
			// Avoiding a cast to IFormattable to avoid boxing.
			switch (value)
			{
				case null:
					return null;

				case bool @bool:
					return @bool ? "1" : "0";

				case byte @byte:
					if (HasFormat) return @byte.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue((int)@byte, CultureInfo.InvariantCulture);

				case decimal @decimal:
					if (HasFormat) return @decimal.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture);

				case double @double:
					if (HasFormat) return @double.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@double, CultureInfo.InvariantCulture);

				case float @float:
					if (HasFormat) return @float.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@float, CultureInfo.InvariantCulture);

				case int @int:
					if (HasFormat) return @int.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@int, CultureInfo.InvariantCulture);

				case long @long:
					if (HasFormat) return @long.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@long, CultureInfo.InvariantCulture);

				case short @short:
					if (HasFormat) return @short.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue((int)@short, CultureInfo.InvariantCulture);

				case string @string:
					return @string;

				default:
					throw new InvalidOperationException($"Unsupported type {value.GetType()}");
			}
		}
		*/

		protected override void OnParametersSet()
		{
			// Require Id?
			if (HasLabel || HasHelp) this.SetIdIfEmpty();

			base.OnParametersSet();
		}
	}
}