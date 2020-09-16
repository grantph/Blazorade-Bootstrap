using System;
using System.Reflection;

namespace Blazorade.Bootstrap.Forms
{
	public static partial class Extensions
	{
		/// <summary>
		/// Get a specific Attribute for a property on an object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target">The target object e.g., Model, or ViewModel</param>
		/// <param name="propertyName">Name of the property e.g., nameof(MyProperty)</param>
		/// <returns>First matching Attribute or null if not found</returns>
		public static T GetAttribute<T>(this object target, string propertyName) where T : Attribute
		{
			// Get Property
			PropertyInfo property = target.GetType().GetProperty(propertyName);

			// Property found? No, throw exception (most likely developer error).
			if (property == null) throw new NotImplementedException(propertyName);

			// Get Attributes (could be multiple)
			T[] attributes = (T[])property.GetCustomAttributes(typeof(T), false);

			// Attributes found? No, quit.
			if (attributes == null || attributes.Length == 0) return null;

			// Return First Attribute
			return attributes[0];
		}
	}
}