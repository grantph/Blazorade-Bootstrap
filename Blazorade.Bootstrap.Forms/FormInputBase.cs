// Copy of https://github.com/dotnet/aspnetcore/blob/8c02467b4a218df3b1b0a69bceb50f5b64f482b1/src/Components/Web/src/Forms/InputBase.cs
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Blazorade.Bootstrap.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazorade.Bootstrap.Forms
{
	/// <summary>
	/// A base class for form input components. This base class automatically
	/// integrates with an <see cref="Forms.EditContext"/>, which must be supplied
	/// as a cascading parameter.
	/// </summary>
	public abstract partial class FormInputBase<TValue> : BootstrapComponentBase
	{
		protected FormInputBase() : base()
		{
			// What's the underlying Type?
			TargetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
		}

		private bool _previousParsingAttemptFailed;
		private ValidationMessageStore _parsingValidationMessages;
		private Type _nullableUnderlyingType;

		protected Type TargetType { get; }

		protected bool BootstrapFormControl { get; set; } = false;

		[CascadingParameter] private EditContext CascadedEditContext { get; set; }

		protected bool? disabled = null;

		/// <summary>
		/// Determine if this control is Disabled. Disabled may be True, False or Null. True are False are absolute states.
		/// If Null, then Model.ModelState will be used. The Model should implement IModelState and set the ModelState.
		/// ModelState.View will result in Disabled = True. Otherwise, Disabled will be False.
		/// It is recommended that you only use this property when overriding BuildRenderTree().
		/// </summary>
		public bool? Disabled
		{
			get => disabled ?? (EditContext.Model is IModelState model && model.ModelState == ModelState.View);
			set => disabled = value;
		}

		[Parameter] public string Format { get; set; }

		protected bool HasFormat { get => !string.IsNullOrEmpty(Format); }

		/// <summary>
		/// Bootstrap Form group label.
		/// </summary>
		[Parameter] public string Label { get; set; }

		protected bool HasLabel { get => !string.IsNullOrEmpty(Label); }

		[Parameter] public string Placeholder { get; set; }

		[Parameter] public bool ReadOnly { get; set; } = false;

		/// <summary>
		/// Bootstrap Input group that adds a prefix to the input control.
		/// </summary>
		[Parameter] public string Prepend { get; set; }

		protected bool HasPrepend { get => !string.IsNullOrEmpty(Prepend); }

		/// <summary>
		/// Bootstrap Input group that adds a suffix to the input control.
		/// </summary>
		[Parameter] public string Append { get; set; }

		protected bool HasAppend { get => !string.IsNullOrEmpty(Append); }

		protected bool HasInputGroup { get => HasPrepend || HasAppend; }

		/// <summary>
		/// Bootstrap Input group that adds a help block to the input control.
		/// </summary>
		[Parameter] public string Help { get; set; }

		/// <summary>
		/// Bootstrap Input group help mode. Defaults to Block.
		/// </summary>
		[Parameter] public Display HelpDisplay { get; set; } = Display.Block;

		protected bool HasHelp { get => !string.IsNullOrEmpty(Help); }

		[Parameter]
		public bool ScreenReaderOnly { get; set; } = false;

		/// <summary>
		/// Size of the control. Defaults to Normal.
		/// </summary>
		[Parameter] public InputSize? Size { get; set; }

		/// <summary>
		/// Gets or sets the value of the input. This should be used with two-way binding.
		/// </summary>
		/// <example>
		/// @bind-Value="model.PropertyName"
		/// </example>
		[Parameter] public TValue Value { get; set; }

		/// <summary>
		/// Gets or sets a callback that updates the bound value.
		/// </summary>
		[Parameter] public EventCallback<TValue> ValueChanged { get; set; }

		/// <summary>
		/// Gets or sets an expression that identifies the bound value.
		/// </summary>
		[Parameter] public Expression<Func<TValue>> ValueExpression { get; set; }

		/// <summary>
		/// Gets the associated <see cref="Forms.EditContext"/>.
		/// </summary>
		protected EditContext EditContext { get; set; }

		/// <summary>
		/// Gets the <see cref="FieldIdentifier"/> for the bound value.
		/// </summary>
		protected FieldIdentifier FieldIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the current value of the input.
		/// </summary>
		protected TValue CurrentValue
		{
			get => Value;
			set
			{
				var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, Value);
				if (hasChanged)
				{
					Value = value;
					_ = ValueChanged.InvokeAsync(value);
					EditContext.NotifyFieldChanged(FieldIdentifier);
				}
			}
		}

		/// <summary>
		/// Gets or sets the current value of the input, represented as a string.
		/// </summary>
		protected string CurrentValueAsString
		{
			get => FormatValueAsString(CurrentValue);
			set
			{
				_parsingValidationMessages?.Clear();

				bool parsingFailed;

				if (_nullableUnderlyingType != null && string.IsNullOrEmpty(value))
				{
					// Assume if it's a nullable type, null/empty inputs should correspond to default(T)
					// Then all subclasses get nullable support almost automatically (they just have to
					// not reject Nullable<T> based on the type itself).
					parsingFailed = false;
					CurrentValue = default;
				}
				else if (TryParseValueFromString(value, out var parsedValue, out var validationErrorMessage))
				{
					parsingFailed = false;
					CurrentValue = parsedValue;
				}
				else
				{
					parsingFailed = true;

					if (_parsingValidationMessages == null)
					{
						_parsingValidationMessages = new ValidationMessageStore(EditContext);
					}

					_parsingValidationMessages.Add(FieldIdentifier, validationErrorMessage);

					// Since we're not writing to CurrentValue, we'll need to notify about modification from here
					EditContext.NotifyFieldChanged(FieldIdentifier);
				}

				// We can skip the validation notification if we were previously valid and still are
				if (parsingFailed || _previousParsingAttemptFailed)
				{
					EditContext.NotifyValidationStateChanged();
					_previousParsingAttemptFailed = parsingFailed;
				}
			}
		}

		/// <summary>
		/// Formats the value as a string. Derived classes can override this to determine the formating used for <see cref="CurrentValueAsString"/>.
		/// </summary>
		/// <param name="value">The value to format.</param>
		/// <returns>A string representation of the value.</returns>
		protected virtual string FormatValueAsString(TValue value)
			=> value?.ToString();

		/// <summary>
		/// Parses a string to create an instance of <typeparamref name="TValue"/>. Derived classes can override this to change how
		/// <see cref="CurrentValueAsString"/> interprets incoming values.
		/// </summary>
		/// <param name="value">The string value to be parsed.</param>
		/// <param name="result">An instance of <typeparamref name="TValue"/>.</param>
		/// <param name="validationErrorMessage">If the value could not be parsed, provides a validation error message.</param>
		/// <returns>True if the value could be parsed; otherwise false.</returns>
		protected abstract bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage);

		/// <summary>
		/// Gets a string that indicates the status of the field being edited. This will include
		/// some combination of "modified", "valid", or "invalid", depending on the status of the field.
		/// </summary>
		private string FieldClass
			=> EditContext.FieldCssClass(FieldIdentifier);

		/// <summary>
		/// Gets a CSS class string that combines the <c>class</c> attribute and <see cref="FieldClass"/>
		/// properties. Derived components should typically use this value for the primary HTML element's
		/// 'class' attribute.
		/// </summary>
		protected virtual string CssClass
		{
			get
			{
				if (Attributes != null &&
					Attributes.TryGetValue("class", out var @class) &&
					!string.IsNullOrEmpty(Convert.ToString(@class)))
				{
					return $"{@class} {FieldClass}";
				}

				return FieldClass; // Never null or empty
			}
		}

		/// <inheritdoc />
		public override Task SetParametersAsync(ParameterView parameters)
		{
			parameters.SetParameterProperties(this);

			if (EditContext == null)
			{
				// This is the first run
				// Could put this logic in OnInit, but its nice to avoid forcing people who override OnInit to call base.OnInit()

				if (CascadedEditContext == null)
				{
					throw new InvalidOperationException($"{GetType()} requires a cascading parameter " +
						$"of type {nameof(Microsoft.AspNetCore.Components.Forms.EditContext)}. For example, you can use {GetType().FullName} inside " +
						$"an {nameof(EditForm)}.");
				}

				if (ValueExpression == null)
				{
					throw new InvalidOperationException($"{GetType()} requires a value for the 'ValueExpression' " +
						$"parameter. Normally this is provided automatically when using 'bind-Value'.");
				}

				EditContext = CascadedEditContext;
				FieldIdentifier = FieldIdentifier.Create(ValueExpression);
				_nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));
			}
			else if (CascadedEditContext != EditContext)
			{
				// Not the first run

				// We don't support changing EditContext because it's messy to be clearing up state and event
				// handlers for the previous one, and there's no strong use case. If a strong use case
				// emerges, we can consider changing this.
				throw new InvalidOperationException($"{GetType()} does not support changing the " +
					$"{nameof(Microsoft.AspNetCore.Components.Forms.EditContext)} dynamically.");
			}

			// For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
			return base.SetParametersAsync(ParameterView.Empty);
		}

		protected override void OnParametersSet()
		{
			// Form Control?
			if (BootstrapFormControl)
			{
				// Add Form-Control
				AddClasses("form-control");

				// Change Size? Default is Normal (do nothing).
				switch (Size)
				{
					// Large
					case InputSize.Large: AddClasses("form-control-lg"); break;

					// Small
					case InputSize.Small: AddClasses("form-control-sm"); break;
				}
			}

			// Placeholder?
			if (!string.IsNullOrEmpty(Placeholder)) AddAttribute("placeholder", Placeholder);

			// ReadOnly?
			if (ReadOnly) AddAttribute("readonly", string.Empty);

			base.OnParametersSet();
		}
	}
}