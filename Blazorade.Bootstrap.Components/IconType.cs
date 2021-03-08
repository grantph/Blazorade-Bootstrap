﻿namespace Blazorade.Bootstrap.Components
{
	/// <summary>
	/// Font Awesome icon styles for Version 5.
	/// </summary>
	/// <seealso cref="https://fontawesome.com/how-to-use/on-the-web/referencing-icons/basic-use"/>
	public enum IconStyle
	{
		/// <summary>
		/// Solid. FREE.
		/// </summary>
		Solid,

		/// <summary>
		/// Regular. Requires PRO Font Awesome license.
		/// </summary>
		Regular,

		/// <summary>
		/// Light. Requires PRO Font Awesome license.
		/// </summary>
		Light,

		/// <summary>
		/// Duotone. Requires PRO Font Awesome license.
		/// </summary>
		Duotone,

		/// <summary>
		/// Brands. FREE - Brand paid Font Awesome to be included.
		/// </summary>
		Brands,
	}

	/// <summary>
	/// Font Awesome icon types.
	/// </summary>
	/// <seealso href="https://fontawesome.com/icons?d=gallery"/>
	/// <remarks>Please ensure the enum entries remain alphabetical.</remarks>
	public enum IconType
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		ArrowDown,
		ArrowLeft,
		ArrowRight,
		ArrowUp,

		Backward,

		Ban,

		Bars,
		MenuBars,

		CaretDown,
		CaretLeft,
		CaretRight,
		CaretUp,

		CcAmex,
		CcDiscover,
		CcDinersClub,
		CcJcb,
		CcMasterCard,
		CcPaypal,
		CcVisa,

		ChevronDown,
		ChevronLeft,
		ChevronRight,
		ChevronUp,

		CircleNotch,

		Clear,
		Close,

		Cog,
		Compass,
		CommentDots,

		Download,

		FastBackward,
		FastForward,

		Forward,

		Info,
		InfoCircle,

		Minus,
		MinusCircle,
		MinusSquare,
		MinusSquareOpen,

		Pause,
		PauseCircle,
		PauseCircleOpen,

		Play,
		PlayCircle,
		PlayCircleOpen,

		Plus,
		PlusCircle,
		PlusSquare,
		PlusSquareOpen,

		Question,
		QuestionCircle,

		Search,

		Sort,
		SortDown,
		SortUp,

		SortAlphaDown,
		SortAlphaUp,

		SortAmountDown,
		SortAmountUp,

		SortNumericDown,
		SortNumericUp,

		Spinner,

		Star,
		StarOpen,
		StarHalf,

		StepBackward,
		StepForward,

		Stop,

		Sync,

		Times,

		Trash,
		TrashOpen,

		Upload,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}