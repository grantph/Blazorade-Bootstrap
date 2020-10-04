using System;
using System.Collections.Generic;

using Blazorade.Bootstrap.Components.Model;
using Blazorade.Bootstrap.Forms;

namespace Blazorade.Bootstrap.Components.Showroom.Host.Shared
{
	public partial class MainMenu
	{
		private List<MenuItem> items = new List<MenuItem>
		{
			new MenuItem
			{
				Text = "Layout",
				Children = new List<MenuItem>
				{
					new MenuItem { Text = "Grid", Url = "grid", Description = "Use the powerful grid system to build mobile-first layouts of all shapes and sizes." },
				}
			},
			new MenuItem
			{
				Text = "Content",
				Children = new List<MenuItem>
				{
					new MenuItem { Text = "Typography", Url = "typography", Description = "Support for typography in Bootstrap."}
				}
			},
			new MenuItem
			{
				Text = "Components",
				Children = new List<MenuItem>
				{
					new MenuItem { Text = "Alerts", Url = "alerts", Description = "The Alert component is used to provide feedback messages, typically in response to user actions." },
					new MenuItem { Text = "Badges", Url = "badges", Description = "The Badge component is used to add counts and labels to other components." },
					new MenuItem { Text = "Breadcrumbs", Url = "breadcrumbs", Description = "The Breadcrumb component visualizes the current page in a navigational hierarchy." },
					new MenuItem { Text = "Buttons", Url = "buttons", Description = "The Button component in Bootstrap is probably one of the most vertsatile component, since it is used for so many things, from forms and dialogs to custom navigation etc." },
					new MenuItem { Text = "Cards", Url = "cards", Description = "The Card component is a flexible container with lots of variants." },
					new MenuItem { Text = "Carousels", Url = "carousels", Description = "A slideshow component for cycling through elements with virtually any kind of content." },
					new MenuItem { Text = "Collapses", Url = "collapses", Description = "The Collapse component is used to toggle the visibility of content." },
					new MenuItem { Text = "Containers", Url = "containers", Description = "The Container component is the foundation for many layout configurations in Bootstrap." },
					new MenuItem { Text = "Dropdowns", Url = "dropdowns", Description = "The Dropdown component is a contextual overlay that displays a list of links and other elements. The Dropdown can be toggled." },
					new MenuItem { Text = "Embeds", Url = "embeds", Description = "The Embed component is used to create responsive video or slideshow embeds." },
					new MenuItem { Text = "List Groups", Url = "listgroups", Description = "The ListGroup component is used to display a series of content in a powerful and flexible way." },
					new MenuItem { Text = "Jumbotrons", Url = "jumbotrons", Description = "The Jumbotron component is a lightweight and flexible component for showcasing content." },
					new MenuItem { Text = "Media", Url = "media", Description = "The Media component is used to build complex and repetitive content where media is positioned alongside content." },
					new MenuItem { Text = "Modals", Url = "modals", Description = "Use the Modal component to add dialogs to your site or application for lightboxes, user notifications or other kinds of custom content." },
					new MenuItem { Text = "Navbars", Url = "navbars", Description = "The Navbar component is used to produce a responsive navigation header with built-in support for collapsing." },
					new MenuItem { Text = "Navs", Url = "navs", Description = "The Nav component provides a simple way to build navigation elements." },
					new MenuItem { Text = "Paginations", Url = "paginations", Description = "The Pagination component is used to create pagination to indicate that a series of related content exists across multiple pages." },
					new MenuItem { Text = "Progress Bars", Url = "progressbars", Description = "The Progress component is used to display progress bars, including stacked bars, animated backgrounds, and text labels." },
					new MenuItem { Text = "Spinners", Url = "spinners", Description = "The Spinner component is used to indicate a loading state of a UI element or page." },
					new MenuItem { Text = "Toasts", Url = "toasts", Description = "The Toast component is used to push notifications to your visitor as a lighweight and customizable alert message." },
				}
			},

			// Forms
			new MenuItem
			{
				Text = "Forms",
				Children = new List<MenuItem>
				{
					new MenuItem { Text = "Complete Example", Url = "forms", Description = $"A complete example using all form controls in edit and view model state." },
					new MenuItem { Text = nameof(FormGroup), Url = nameof(FormGroup).ToLower(), Description = $"The {nameof(FormGroup)} component used to control layout of other form components." },
					new MenuItem { Text = nameof(FormInputTextBox), Url = nameof(FormInputTextBox).ToLower(), Description = $"The {nameof(FormInputTextBox)} component is used to edit text. It combines single and multiline into one simple to use component." },
					new MenuItem { Text = nameof(FormInputText), Url = nameof(FormInputText).ToLower(), Description = $"The {nameof(FormInputText)} component is used to edit single line text." },
					new MenuItem { Text = nameof(FormInputTextArea), Url = nameof(FormInputTextArea).ToLower(), Description = $"The {nameof(FormInputTextArea)} component is used to edit multi line text." },
					new MenuItem { Text = nameof(FormInputNumber<int>), Url = nameof(FormInputNumber<int>).ToLower(), Description = $"The {nameof(FormInputNumber<int>)} component implements a number edit control." },
					new MenuItem { Text = nameof(FormInputSelect<string>), Url = nameof(FormInputSelect<string>).ToLower(), Description = $"The {nameof(FormInputSelect<string>)} component implements a input select control." },
					new MenuItem { Text = nameof(FormInputCheckbox), Url = nameof(FormInputCheckbox).ToLower(), Description = $"The {nameof(FormInputCheckbox)} component implements a checkbox." },
					new MenuItem { Text = nameof(FormInputFile), Url = nameof(FormInputFile).ToLower(), Description = $"The {nameof(FormInputFile)} component implements a file upload control." },
					new MenuItem { Text = nameof(FormInputDate<DateTime>), Url = nameof(FormInputDate<DateTime>).ToLower(), Description = $"The {nameof(FormInputDate<DateTime>)} component implements a date control." },
					new MenuItem { Text = nameof(FormInputTime<DateTime>), Url = nameof(FormInputTime<DateTime>).ToLower(), Description = $"The {nameof(FormInputTime<DateTime>)} component implements a time control." },
					new MenuItem { Text = "ModelState", Url = "modelstate", Description = $"More detail on IModelState to control edit and view modes of a model." },
					new MenuItem { Text = "Complex Model Validation", Url = "complexmodels", Description = $"Pointers on how to perform validation on complex models." },
					new MenuItem { Text = "Inherited Model Validation", Url = "inheritedmodels", Description = $"Pointers on how to perform validation on inherited models." },
					new MenuItem { Text = "Coming Soon - FormAutoComplete", Url = "", Description = $"" },
					new MenuItem { Text = "Coming Soon - Icon", Url = "", Description = $"" },
				}
			},

			new MenuItem
			{
				Text = "Utilities",
				Children = new List<MenuItem>
			{
					new MenuItem { Text = "Sizing", Url = "sizing", Description = "The sizing utility makes it easy to make components equally wide or high." }
				}
			},
			//new MenuItem
			//{
			//    Text = "Work in Progress",
			//    IsDisabled = true,
			//    Children = new List<MenuItem>
			//    {
			//    }
			//}
		};
	}
}