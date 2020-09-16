﻿using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace Blazorade.Bootstrap.Components
{
	/// <summary>
	/// The Button component allows you to render various types of buttons.
	/// </summary>
	public partial class Button
	{
		/// <summary>
		/// The callback that is called when the button is clicked.
		/// </summary>
		[Parameter]
		public EventCallback<Button> OnClicked { get; set; }

		/// <summary>
		/// Specifies whether the button appears active.
		/// </summary>
		[Parameter]
		public bool IsActive { get; set; }

		/// <summary>
		/// Specifies whether the button is a block-level button.
		/// </summary>
		[Parameter]
		public bool IsBlockLevel { get; set; }

		/// <summary>
		/// Specifies whether the button is disabled.
		/// </summary>
		[Parameter]
		public bool IsDisabled { get; set; }

		/// <summary>
		/// Specifies whether the button is styled as an outline button.
		/// </summary>
		[Parameter]
		public bool IsOutline { get; set; }

		/// <summary>
		/// Specifies whether the button is a submit button. The default type is <c>button</c>.
		/// </summary>
		[Parameter]
		public bool IsSubmit { get; set; }

		/// <summary>
		/// Specifies the size for the button.
		/// </summary>
		[Parameter]
		public ButtonSize? Size { get; set; }

		/// <summary>
		/// Fires the <see cref="OnClicked"/> event.
		/// </summary>
		protected virtual async Task OnClickedAsync()
		{
			await this.OnClicked.InvokeAsync(this);
		}

		/// <summary>
		/// </summary>
		protected override void OnParametersSet()
		{
			this.AddClasses(ClassNames.Buttons.Button);

			if (this.Color.HasValue)
			{
				this.AddClasses(this.GetColorClassName(prefix: !this.IsOutline ? ClassNames.Buttons.Button : ClassNames.Buttons.OutlineButton, color: this.Color));
			}

			if (this.IsBlockLevel)
			{
				this.AddClasses(ClassNames.Buttons.BlockLevel);
			}

			if (this.IsActive)
			{
				this.AddClasses(ClassNames.Active);
			}

			switch (this.Size.GetValueOrDefault())
			{
				case ButtonSize.Large:
					this.AddClasses(ClassNames.Buttons.Large);
					break;

				case ButtonSize.Small:
					this.AddClasses(ClassNames.Buttons.Small);
					break;
			}

			if (this.IsDisabled)
			{
				this.AddAttribute("disabled", "disabled");
			}

			if (!this.IsSubmit)
			{
				this.AddAttribute("type", "button");
			}
			else
			{
				this.AddAttribute("type", "submit");
			}

			base.OnParametersSet();
		}
	}

	/// <summary>
	/// Defines different sizes for a button.
	/// </summary>
	public enum ButtonSize
	{
		/// <summary>
		/// The default size.
		/// </summary>
		Normal = 0,

		/// <summary>
		/// Large button.
		/// </summary>
		Large = 1,

		/// <summary>
		/// Small button.
		/// </summary>
		Small = 2
	}
}