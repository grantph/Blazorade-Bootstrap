using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;

namespace Blazorade.Bootstrap.Components
{
	public partial class Icon
	{
		/// <summary>
		/// Font Awesome version.
		/// </summary>
		[Parameter] public int Version { get; set; } = 4;

		/// <summary>
		/// Font Awesome icon style.
		/// </summary>
		[Parameter] public IconStyle Style { get; set; } = IconStyle.Solid;

		/// <summary>
		/// Font Awesome icon type.
		/// </summary>
		[Parameter] public IconType Type { get; set; }

		/// <summary>
		/// Callback for when the NavItem is clicked.
		/// </summary>
		[Parameter] public EventCallback<Icon> Clicked { get; set; }

		/// <summary>
		/// Enable spinning icon. Uses Css frame rendering.
		/// </summary>
		[Parameter] public bool IsSpinning { get; set; } = false;

		/// <summary>
		/// Convert style class names from Type to string.
		/// </summary>
		private string IconStyleClass =>
			Version switch
			{
				// v4 style (assuming 4.7.0)
				4 => "fa",

				// v5 style (assuming 5.14.0)
				5 => Style switch
				{
					IconStyle.Brands => "fab",
					IconStyle.Duotone => "fad",
					IconStyle.Light => "fal",
					IconStyle.Regular => "far",
					IconStyle.Solid => "fas",

					// Default Style v5
					_ => throw new NotImplementedException(Style.ToString()),
				},

				// Default Version
				_ => throw new NotImplementedException($"Version {Version} not implemented")
			};

		private string IconTypeClass =>
#pragma warning disable IDE0072 // Add missing cases.
			// They are NOT missing. Analyzer is doing bad job of detecting "var x" statements.
			Type switch
#pragma warning restore IDE0072 // Add missing cases
			{
				IconType.ArrowDown => "fa-arrow-down",
				IconType.ArrowLeft => "fa-arrow-left",
				IconType.ArrowRight => "fa-arrow-right",
				IconType.ArrowUp => "fa-arrow-up",

				IconType.Backward => "fa-backward",

				IconType.Ban => "fa-ban",

				var x when x == IconType.Bars || x == IconType.MenuBars => "fa-bars",

				IconType.CaretDown => "fa-caret-down",
				IconType.CaretLeft => "fa-caret-left",
				IconType.CaretRight => "fa-caret-right",
				IconType.CaretUp => "fa-caret-up",

				IconType.CcAmex => "fa-cc-amex",
				IconType.CcDiscover => "fa-cc-discover",
				IconType.CcDinersClub => "fa-cc-diners-club",
				IconType.CcJcb => "fa-cc-jcb",
				IconType.CcMasterCard => "fa-cc-mastercard",
				IconType.CcPaypal => "fa-cc-paypal",
				IconType.CcVisa => "fa-cc-visa",

				IconType.ChevronDown => "fa-chevron-down",
				IconType.ChevronLeft => "fa-chevron-left",
				IconType.ChevronRight => "fa-chevron-right",
				IconType.ChevronUp => "fa-chevron-up",

				IconType.CircleNotch => "fa-circle-notch",

				IconType.Cog => "fa-cog",
				IconType.Compass => "fa-compass",

				IconType.Download => "fa-download",

				IconType.FastBackward => "fa-fast-backward",
				IconType.FastForward => "fa-fast-forward",

				IconType.Forward => "fa-forward",

				IconType.Minus => "fa-minus",
				IconType.MinusCircle => "fa-minus-circle",
				IconType.MinusSquare => "fa-minus-square",
				IconType.MinusSquareOpen => "fa-minus-square-o",

				IconType.Pause => "fa-pause",
				IconType.PauseCircle => "fa-pause-circle",
				IconType.PauseCircleOpen => "fa-pause-circle-o",

				IconType.Play => "fa-play",
				IconType.PlayCircle => "fa-play-circle",
				IconType.PlayCircleOpen => "fa-play-circle-o",

				IconType.Plus => "fa-plus",
				IconType.PlusCircle => "fa-plus-circle",
				IconType.PlusSquare => "fa-plus-square",
				IconType.PlusSquareOpen => "fa-plus-square-o",

				IconType.Search => "fa-search",

				IconType.Sort => "fa-sort",
				IconType.SortDown => "fa-sort-down",
				IconType.SortUp => "fa-sort-up",

				IconType.SortAlphaDown => "fa-sort-alpha-down",
				IconType.SortAlphaUp => "fa-sort-alpha-up",

				IconType.SortAmountDown => "fa-sort-amount-down",
				IconType.SortAmountUp => "fa-sort-amount-up",

				IconType.SortNumericDown => "fa-sort-numeric-down",
				IconType.SortNumericUp => "fa-sort-numeric-up",

				IconType.Spinner => "fa-spinner",

				IconType.StepBackward => "fa-step-backward",
				IconType.StepForward => "fa-step-forward",

				IconType.Star => "fa-star",
				IconType.StarOpen => "fa-star-o",
				IconType.StarHalf => "fa-star-half",

				IconType.Sync => "fa-sync",

				var x when x == IconType.Clear || x == IconType.Close || x == IconType.Times => "fa-times",

				IconType.Trash => "fa-trash",
				IconType.TrashOpen => "fa-trash-o",

				IconType.Upload => "fa-upload",

				// Default
				_ => throw new NotImplementedException(Type.ToString())
			};

		/// <inheritdoc/>
		protected override void OnParametersSet()
		{
			List<string> classes = new List<string> { IconStyleClass, IconTypeClass };

			// Optional Spinning state
			if (IsSpinning)
			{
				classes.Add("fa-spinning");
			}

			// Add Classes
			AddClasses(classes.ToArray());

			base.OnParametersSet();
		}
	}
}