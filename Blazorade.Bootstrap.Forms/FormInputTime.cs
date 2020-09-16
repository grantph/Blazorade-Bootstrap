using System;
using System.Globalization;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	/// <summary>
	/// An input component for editing date values.
	/// Supported types are <see cref="DateTime"/> and <see cref="DateTimeOffset"/>.
	/// </summary>
	public class FormInputTime<TValue> : FormInputBase<TValue>
	{
		public FormInputTime() : base()
		{
			// This is a Bootstrap 4 Form Control
			base.BootstrapFormControl = true;
			base.Format = "HH:mm"; // Compatible with HTML time inputs
		}

		/// <summary>
		/// Gets or sets the error message used when displaying an a parsing error.
		/// </summary>
		[Parameter] public string ParsingErrorMessage { get; set; } = "The {0} field must be a time.";

		/// <summary>
		/// Minimum time in seconds.
		/// </summary>
		[Parameter] public TimeSpan? Min { get; set; } = null;

		/// <summary>
		/// Maximum time in seconds.
		/// </summary>
		[Parameter] public TimeSpan? Max { get; set; } = null;

		/// <summary>
		/// Time step in seconds.
		/// </summary>
		[Parameter] public int? Step { get; set; } = null;

		/// <summary>
		/// Is field required?
		/// </summary>
		[Parameter] public bool Required { get; set; } = true;

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
				builder.AddAttribute(seq++, "type", "time");
				builder.AddAttribute(seq++, "class", CssClass); // This will overwrite version in Attributes
				builder.AddAttribute(seq++, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
				builder.AddAttribute(seq++, "value", BindConverter.FormatValue(CurrentValueAsString));

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
		protected override string FormatValueAsString(TValue value)
		{
			switch (value)
			{
				case TimeSpan timeSpanValue:
					return timeSpanValue.ToString(Format.Replace(":", "\\:").ToLower(), CultureInfo.InvariantCulture);

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

			if (targetType == typeof(TimeSpan))
			{
				success = TryParseTimeSpan(value, out result);

				if (success)
				{
					// After Min?
					if (Min.HasValue)
					{
						success &= ((TimeSpan)(object)result) >= Min.Value;
					}

					// Before Max?
					if (Max.HasValue)
					{
						success &= ((TimeSpan)(object)result) <= Max.Value;
					}
				}
			}
			else
			{
				throw new InvalidOperationException($"The type '{targetType}' is not a supported time type.");
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

		private static bool TryParseTimeSpan(string value, out TValue result)
		{
			bool success = TimeSpan.TryParse(value, out var parsedValue);
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
			if (HasLabel || HasHelp) SetIdIfEmpty();

			// Min and/or Max?
			if (Min.HasValue) AddAttribute("min", Min.Value.ToString("hh\\:mm\\:ss"));
			if (Max.HasValue) AddAttribute("max", Max.Value.ToString("hh\\:mm\\:ss"));

			// Step (default: any)
			if (Step.HasValue) AddAttribute("step", Step.Value.ToString());

			// Required?
			if (Required) AddAttribute("required", string.Empty);

			base.OnParametersSet();
		}
	}
}