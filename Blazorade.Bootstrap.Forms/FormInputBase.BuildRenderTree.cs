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

			int seq = 0;

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

		/// <summary>Builds the Bootstrap Help block.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreeHelp(RenderTreeBuilder builder)
		{
			// No Help?
			if (!HasHelp) return;

			int seq = 0;

			// Small
			builder.OpenElement(seq++, "small");
			builder.AddAttribute(seq++, "id", $"{Id}-help");
			builder.AddAttribute(seq++, "class", HelpDisplay == Display.Block ? "form-text text-muted" : "text-muted");
			builder.AddContent(seq++, Help);
			builder.CloseElement();
		}

		/// <summary>Builds the Bootstrap Label block.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreeLabel(RenderTreeBuilder builder)
		{
			if (!HasLabel) return;

			int seq = 0;

			// Label
			builder.OpenElement(seq++, "label");
			if (ScreenReaderOnly) builder.AddAttribute(seq++, "class", "sr-only");
			builder.AddAttribute(seq++, "for", Id);
			builder.AddContent(seq++, Label);
			builder.CloseElement();
		}

		/// <summary>Stop building the Bootstrap Input Group. Should always be paired with BuildRenderTreeInputGroupOpen.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreeInputGroupClose(RenderTreeBuilder builder)
		{
			if (!HasInputGroup) return;

			int seq = 0;

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

			// /Div
			builder.CloseElement();
		}

		/// <summary>Start building the Bootstrap Input Group. Should always be paired with BuildRenderTreeInputGroupClose.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreeInputGroupOpen(RenderTreeBuilder builder)
		{
			if (!HasInputGroup) return;

			int seq = 0;

			// Div
			builder.OpenElement(seq++, "div");
			builder.AddAttribute(seq++, "class", "input-group");
		}

		/// <summary>Build the Bootstrap Prepend block.</summary>
		/// <param name="builder">The builder.</param>
		protected virtual void BuildRenderTreePrepend(RenderTreeBuilder builder)
		{
			if (!HasPrepend) return;

			int seq = 0;

			// Div
			builder.OpenElement(seq++, "div");
			builder.AddAttribute(seq++, "class", "input-group-prepend");

			{
				// Span
				builder.OpenElement(seq++, "span");
				builder.AddAttribute(seq++, "class", "input-group-text");
				builder.AddAttribute(seq++, "id", $"{Id}-prepend");
				builder.AddContent(seq++, Prepend);
				builder.CloseElement();
			}

			builder.CloseElement();
		}
	}
}