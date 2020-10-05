using System;
using System.ComponentModel.DataAnnotations;

using Blazorade.Bootstrap.Forms;

namespace Blazorade.Bootstrap.Components.Showroom.Forms.Models
{
	public class CompleteExample : IModelState
	{
		public ModelState ModelState { get; set; } = ModelState.Edit;

		[Required(ErrorMessage = "Name is required"), StringLength(40, MinimumLength = 2)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Title is required"), MinLength(2), MaxLength(10)]
		public string Title { get; set; }

		public bool Checkbox1 { get; set; } = false;

		public DateTime DateTime1 { get; set; } = DateTime.Now;

		/// <summary>
		/// Date1 maps to DateTime1
		/// </summary>
		public DateTime Date1
		{
			get => new DateTime(DateTime1.Year, DateTime1.Month, DateTime1.Day);
			set => DateTime1 = new DateTime(value.Year, value.Month, value.Day, DateTime1.Hour, DateTime1.Minute, DateTime1.Second);
		}

		/// <summary>
		/// Time1 maps to DateTime1
		/// </summary>
		public TimeSpan Time1
		{
			get => DateTime1.TimeOfDay;
			set => DateTime1 = new DateTime(DateTime1.Year, DateTime1.Month, DateTime1.Day, value.Hours, value.Minutes, value.Seconds);
		}
	}
}