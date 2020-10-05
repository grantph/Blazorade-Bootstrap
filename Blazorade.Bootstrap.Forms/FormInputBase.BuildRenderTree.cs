using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorade.Bootstrap.Forms
{
	public partial class FormInputBase<TValue>
	{
		/// <summary>Builds the Bootstrap Append block.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreeAppend(RenderTreeBuilder builder)
		{
			if (!HasAppend) return;

			// <div>
			builder.OpenElement(100, Html.DIV);
			builder.AddAttribute(101, Html.CLASS, "input-group-append");

			// <span>
			builder.OpenElement(102, Html.SPAN);
			builder.AddAttribute(103, Html.CLASS, Bootstrap.INPUT_GROUP_TEXT);
			builder.AddContent(104, Append);
			builder.CloseElement();
			// </span>

			// </div>
			builder.CloseElement();
		}

		/// <summary>Builds the Bootstrap Help block.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreeHelp(RenderTreeBuilder builder)
		{
			// No Help?
			if (!HasHelp) return;

			// <small>
			builder.OpenElement(110, "small");
			builder.AddAttribute(111, "id", $"{Id}-help");
			builder.AddAttribute(112, Html.CLASS, HelpDisplay == Display.Block ? "form-text text-muted" : "text-muted");
			builder.AddContent(113, Help);
			builder.CloseElement();
			// </small>
		}

		/// <summary>Builds the Bootstrap Label block.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreeLabel(RenderTreeBuilder builder)
		{
			if (!HasLabel) return;

			// <label>
			builder.OpenElement(120, Html.LABEL);
			if (ScreenReaderOnly) builder.AddAttribute(121, Html.CLASS, "sr-only");
			builder.AddAttribute(122, "for", Id);
			builder.AddContent(123, Label);
			builder.CloseElement();
			// </label>
		}

		/// <summary>Stop building the Bootstrap Input Group. Should always be paired with BuildRenderTreeInputGroupOpen.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreeInputGroupClose(RenderTreeBuilder builder)
		{
			if (!HasInputGroup) return;

			if (HasAppend)
			{
				// <div>
				builder.OpenElement(133, Html.DIV);
				builder.AddAttribute(134, Html.CLASS, "input-group-append");

				// <span>
				builder.OpenElement(135, Html.SPAN);
				builder.AddAttribute(136, Html.CLASS, Bootstrap.INPUT_GROUP_TEXT);
				builder.AddContent(137, Append);
				builder.CloseElement();
				// </span>

				// </div>
				builder.CloseElement();
			}

			// </div>
			builder.CloseElement();
		}

		/// <summary>Start building the Bootstrap Input Group. Should always be paired with BuildRenderTreeInputGroupClose.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreeInputGroupOpen(RenderTreeBuilder builder)
		{
			if (!HasInputGroup) return;

			// <div>
			builder.OpenElement(130, Html.DIV);
			builder.AddAttribute(131, Html.CLASS, Bootstrap.INPUT_GROUP);
		}

		/// <summary>Build the Bootstrap Prepend block.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreePrepend(RenderTreeBuilder builder)
		{
			if (!HasPrepend) return;

			// <div>
			builder.OpenElement(140, Html.DIV);
			builder.AddAttribute(141, Html.CLASS, Bootstrap.INPUT_GROUP_PREPEND);

			// <span>
			builder.OpenElement(142, Html.SPAN);
			builder.AddAttribute(143, Html.CLASS, Bootstrap.INPUT_GROUP_TEXT);
			builder.AddAttribute(144, "id", $"{Id}-prepend");
			builder.AddContent(145, Prepend);
			builder.CloseElement();
			// </span>

			// </div>
			builder.CloseElement();
		}
	}
}