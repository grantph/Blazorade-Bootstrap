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
			builder.OpenElement(100, "div");
			builder.AddAttribute(101, "class", "input-group-append");

			// <span>
			builder.OpenElement(102, "span");
			builder.AddAttribute(103, "class", "input-group-text");
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
			builder.AddAttribute(112, "class", HelpDisplay == Display.Block ? "form-text text-muted" : "text-muted");
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
			builder.OpenElement(120, "label");
			if (ScreenReaderOnly) builder.AddAttribute(121, "class", "sr-only");
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
				builder.OpenElement(133, "div");
				builder.AddAttribute(134, "class", "input-group-append");

				// <span>
				builder.OpenElement(135, "span");
				builder.AddAttribute(136, "class", "input-group-text");
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
			builder.OpenElement(130, "div");
			builder.AddAttribute(131, "class", "input-group");
		}

		/// <summary>Build the Bootstrap Prepend block.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreePrepend(RenderTreeBuilder builder)
		{
			if (!HasPrepend) return;

			// <div>
			builder.OpenElement(140, "div");
			builder.AddAttribute(141, "class", "input-group-prepend");

			// <span>
			builder.OpenElement(142, "span");
			builder.AddAttribute(143, "class", "input-group-text");
			builder.AddAttribute(144, "id", $"{Id}-prepend");
			builder.AddContent(145, Prepend);
			builder.CloseElement();
			// </span>

			// </div>
			builder.CloseElement();
		}
	}
}