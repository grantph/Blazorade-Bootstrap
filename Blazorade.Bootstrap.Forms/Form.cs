using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazorade.Bootstrap.Forms
{
	/// <summary>
	/// Form replaces EditForm control, and implements ModelState which allows the UI to be rendered in View or Edit state.
	/// For Edit use, set the Model property. For View used, set the ViewModel property.
	/// </summary>
	/// <remarks>
	/// We could implement ViewForm, EditForm, etc. however, it seemed to be more generic to go with Form and set the
	/// appropriate property. By simply calling it Form, we could easily switch between Edit and View without entirely
	/// new objects.
	/// </remarks>
	public class Form : EditForm
	{
		/// <summary>
		/// Set the ModelState to Edit, and apply ViewMode to the underlying Model. This replaces and
		/// references EditForm.Model.
		/// </summary>
		[Parameter]
		public object EditModel
		{
			get => base.Model;
			set
			{
				base.Model = value;

				if (base.Model is IModelState model) model.ModelState = ModelState.Edit;
			}
		}

		/// <summary>
		/// Set the ModelState to View, and apply ViewMode to the underlying Model. This replaces and
		/// references EditForm.Model.
		/// </summary>
		[Parameter]
		public object ViewModel
		{
			get => base.Model;
			set
			{
				base.Model = value;

				if (base.Model is IModelState model) model.ModelState = ModelState.View;
			}
		}

		[Parameter]
		public ModelState State
		{
			get => base.Model is IModelState model ? model.ModelState : ModelState.Edit;
			set
			{
				if (base.Model is IModelState model) model.ModelState = value;
			}
		}
	}
}