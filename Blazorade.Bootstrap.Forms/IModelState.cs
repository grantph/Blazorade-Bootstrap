namespace Blazorade.Bootstrap.Forms
{
	/// <summary>
	/// Implement this interface on each Model to support ModelState.
	/// </summary>
	public interface IModelState
	{
		ModelState ModelState { get; set; }
	}
}