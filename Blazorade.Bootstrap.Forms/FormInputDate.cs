// Copy of https://github.com/dotnet/aspnetcore/blob/8c02467b4a218df3b1b0a69bceb50f5b64f482b1/src/Components/Web/src/Forms/InputDate.cs
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	/// <summary>
	/// An input component for editing date values.
	/// Supported types are <see cref="DateTime"/> and <see cref="DateTimeOffset"/>.
	/// </summary>
	public class FormInputDate<TValue> : FormInputBase<TValue>
	{
		public FormInputDate() : base()
		{
			// This is a Bootstrap 4 Form Control
			base.BootstrapFormControl = true;
			base.Format = "yyyy-MM-dd"; // Compatible with HTML date inputs
		}

		/// <summary>
		/// Gets or sets the error message used when displaying an a parsing error.
		/// </summary>
		[Parameter] public string ParsingErrorMessage { get; set; } = "The {0} field must be a date.";

		/// <summary>
		/// Earliest allowed date.
		/// </summary>
		[Parameter] public DateTime? Min { get; set; } = null;

		/// <summary>
		/// Latest allowed date.
		/// </summary>
		[Parameter] public DateTime? Max { get; set; } = null;

		/// <summary>
		/// Step in days. Default is 1 day.
		/// </summary>
		[Parameter] public int? Step { get; set; } = null;

		/// <summary>
		/// Is this field is required? Defaults to True.
		/// </summary>
		[Parameter] public bool Required { get; set; } = true;

		/// <inheritdoc />
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			// Label
			BuildRenderTreeLabel(builder);

			if (HasInputGroup)
			{
				// Div
				builder.OpenElement(0, Html.DIV);
				builder.AddAttribute(1, Html.CLASS, Bootstrap.INPUT_GROUP);
			}

			if (HasPrepend)
			{
				// Div
				builder.OpenElement(2, Html.DIV);
				builder.AddAttribute(3, Html.CLASS, Bootstrap.INPUT_GROUP_PREPEND);

				// Span
				builder.OpenElement(4, Html.SPAN);
				builder.AddAttribute(5, Html.CLASS, Bootstrap.INPUT_GROUP_TEXT);
				builder.AddContent(6, Prepend);
				builder.CloseElement();

				builder.CloseElement();
			}

			// Input
			builder.OpenElement(10, Html.INPUT);
			builder.AddMultipleAttributes(11, Attributes);
			builder.AddAttribute(12, Html.TYPE, Html.DATE);
			builder.AddAttribute(13, Html.CLASS, CssClass); // Overwrite class in Attributes
			Console.WriteLine(CurrentValueAsString);
			builder.AddAttribute(14, Html.VALUE, CurrentValueAsString);
			builder.AddAttribute(15, Html.ONCHANGE, EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

			// Disabled?
			if (Disabled ?? false) builder.AddAttribute(17, Html.DISABLED, string.Empty);

			// Help
			if (HasHelp) builder.AddAttribute(18, Html.ARIA_DESCRIBEDBY, $"{Id}-help");

			builder.CloseElement();

			if (HasAppend)
			{
				// Div
				builder.OpenElement(20, Html.DIV);
				builder.AddAttribute(21, Html.CLASS, "input-group-append");

				// Span
				builder.OpenElement(22, Html.SPAN);
				builder.AddAttribute(23, Html.CLASS, Bootstrap.INPUT_GROUP_TEXT);
				builder.AddContent(24, Append);
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
		protected override string FormatValueAsString(TValue value)
		{
			switch (value)
			{
				case DateTime dateTimeValue:
					return BindConverter.FormatValue(dateTimeValue, Format, CultureInfo.CurrentUICulture);

				case DateTimeOffset dateTimeOffsetValue:
					return BindConverter.FormatValue(dateTimeOffsetValue, Format, CultureInfo.CurrentUICulture);

				default:
					return string.Empty; // Handles null for Nullable<DateTime>, etc.
			}
		}

		/// <inheritdoc />
		protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
		{
			// Unwrap nullable types. We don't have to deal with receiving empty values for nullable
			// types here, because the underlying InputBase already covers that.
			var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);

			bool success;
			if (targetType == typeof(DateTime))
			{
				success = TryParseDateTime(value, out result);

				if (success)
				{
					if (Min.HasValue)
					{
						//Console.WriteLine($"{((DateTime)(object)result):u} < {Min.Value:u}");
						if (((DateTime)(object)result) < Min.Value)
						{
							// Limit Min
							result = (TValue)(object)Min.Value;
							validationErrorMessage = null;
							return false;
						}
					}

					if (Max.HasValue)
					{
						//Console.WriteLine($"{((DateTime)(object)result):u} > {Max.Value:u}");
						if (((DateTime)(object)result) > Max.Value)
						{
							// Limit Max
							result = (TValue)(object)Max.Value;
							validationErrorMessage = null;
							return false;
						}
					}
				}
			}
			else if (targetType == typeof(DateTimeOffset))
			{
				success = TryParseDateTimeOffset(value, out result);
			}
			else
			{
				throw new InvalidOperationException($"The type '{targetType}' is not a supported date type.");
			}

			if (success)
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

		private const string ISO_FORMAT = "yyyy-MM-dd";

		private static bool TryParseDateTime(string value, out TValue result)
		{
			// Try Culture format
			var success = BindConverter.TryConvertToDateTime(value, CultureInfo.CurrentUICulture, out DateTime parsedValue);

			// Success?
			if (!success)
			{
				// Try ISO format
				success = BindConverter.TryConvertToDateTime(value, CultureInfo.CurrentUICulture, ISO_FORMAT, out parsedValue);
			}

			if (success)
			{
				result = (TValue)(object)parsedValue;
				return true;
			}
			else
			{
				result = default;
				return false;
			}
		}

		private static bool TryParseDateTimeOffset(string value, out TValue result)
		{
			// Try Culture format
			var success = BindConverter.TryConvertToDateTimeOffset(value, CultureInfo.CurrentUICulture, out DateTimeOffset parsedValue);

			// Success?
			if (!success)
			{
				// Try ISO format
				success = BindConverter.TryConvertToDateTimeOffset(value, CultureInfo.CurrentUICulture, ISO_FORMAT, out parsedValue);
			}

			if (success)
			{
				result = (TValue)(object)parsedValue;
				return true;
			}
			else
			{
				result = default;
				return false;
			}
		}

		protected override void OnParametersSet()
		{
			// Require Id?
			if (HasLabel || HasHelp) this.SetIdIfEmpty();

			// Min & Max?
			if (Min.HasValue) AddAttribute("min", Min.Value.ToString(ISO_FORMAT));
			if (Max.HasValue) AddAttribute("max", Max.Value.ToString(ISO_FORMAT));

			// Step?
			if (Step.HasValue) AddAttribute("step", Step.Value);    // step="any" could also be used for default.

			base.OnParametersSet();
		}
	}
}