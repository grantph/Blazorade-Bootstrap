// Copy of https://github.com/dotnet/aspnetcore/blob/8c02467b4a218df3b1b0a69bceb50f5b64f482b1/src/Components/Web/src/Forms/InputNumber.cs
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	/// <summary>
	/// An input component for editing numeric values.
	/// Supported numeric types are <see cref="int"/>, <see cref="long"/>, <see cref="float"/>, <see cref="double"/>, <see cref="decimal"/>.
	/// </summary>
	public class FormInputNumber<TValue> : FormInputBase<TValue>
	{
		//private static string _stepAttributeValue; // Null by default, so only allows whole numbers as per HTML spec

		static FormInputNumber()
		{
			// Unwrap Nullable<T>, because InputBase already deals with the Nullable aspect
			// of it for us. We will only get asked to parse the T for nonempty inputs.
			var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
			if (targetType == typeof(byte) ||
				targetType == typeof(short) ||
				targetType == typeof(int) ||
				targetType == typeof(long) ||
				targetType == typeof(float) ||
				targetType == typeof(double) ||
				targetType == typeof(decimal))
			{
				//_stepAttributeValue = "any";
			}
			else
			{
				throw new InvalidOperationException($"The type '{targetType}' is not a supported numeric type.");
			}
		}

		public FormInputNumber() : base()
		{
			base.BootstrapFormControl = true;
		}

		/// <summary>
		/// Gets or sets the error message used when displaying an a parsing error.
		/// </summary>
		[Parameter] public string ParsingErrorMessage { get; set; } = "The {0} field must be a number.";

		[Parameter]
		public double? Min { get; set; } = null;

		[Parameter]
		public double? Max { get; set; } = null;

		[Parameter]
		public double? Step { get; set; } = null;

		/// <inheritdoc />
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			int seq = 0;

			// Label
			BuildRenderTreeLabel(builder);

			if (HasInputGroup)
			{
				// Div
				builder.OpenElement(seq++, "div");
				builder.AddAttribute(seq++, "class", "input-group");
			}

			if (HasPrepend)
			{
				// Div
				builder.OpenElement(seq++, "div");
				builder.AddAttribute(seq++, "class", "input-group-prepend");

				{
					// Span
					builder.OpenElement(seq++, "span");
					builder.AddAttribute(seq++, "class", "input-group-text");
					builder.AddContent(seq++, Prepend);
					builder.CloseElement();
				}

				builder.CloseElement();
			}

			{
				// Input
				builder.OpenElement(seq++, "input");
				builder.AddMultipleAttributes(seq++, Attributes);
				builder.AddAttribute(seq++, "type", "number");
				builder.AddAttribute(seq++, "value", BindConverter.FormatValue(CurrentValueAsString));
				builder.AddAttribute(seq++, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
				builder.AddAttribute(seq++, "class", CssClass); // Overwrite class in Attributes

				// Disabled?
				if (Disabled ?? false) builder.AddAttribute(seq++, "disabled", string.Empty);

				// Help
				if (HasHelp) builder.AddAttribute(seq++, "aria-describedby", $"{Id}-help");

				builder.CloseElement();
			}

			if (HasAppend)
			{
				// Div
				builder.OpenElement(seq++, "div");
				builder.AddAttribute(seq++, "class", "input-group-append");

				{
					// Span
					builder.OpenElement(seq++, "span");
					builder.AddAttribute(seq++, "class", "input-group-text");
					builder.AddContent(seq++, Append);
					builder.CloseElement();
				}

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
			if (string.IsNullOrEmpty(value))
			{
				validationErrorMessage = null;
				result = default;
				return true;
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
		}

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

				case short @short:
					if (HasFormat) return @short.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue((int)@short, CultureInfo.InvariantCulture);

				case int @int:
					if (HasFormat) return @int.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@int, CultureInfo.InvariantCulture);

				case long @long:
					if (HasFormat) return @long.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@long, CultureInfo.InvariantCulture);

				case float @float:
					if (HasFormat) return @float.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@float, CultureInfo.InvariantCulture);

				case double @double:
					if (HasFormat) return @double.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@double, CultureInfo.InvariantCulture);

				case decimal @decimal:
					if (HasFormat) return @decimal.ToString(Format, CultureInfo.InvariantCulture);
					return BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture);

				default:
					throw new InvalidOperationException($"Unsupported type {value.GetType()}");
			}
		}

		protected override void OnParametersSet()
		{
			// Require Id?
			if (HasLabel || HasHelp) this.SetIdIfEmpty();

			// Min and/or Max?
			if (Min.HasValue) AddAttribute("min", Min);
			if (Max.HasValue) AddAttribute("max", Max);

			// Step (default: any)
			AddAttribute("step", Step.HasValue ? Step.Value.ToString() : "any");

			base.OnParametersSet();
		}
	}
}