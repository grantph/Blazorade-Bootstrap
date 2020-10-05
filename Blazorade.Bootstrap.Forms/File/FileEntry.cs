using System;
using System.IO;
using System.Threading.Tasks;

namespace Blazorade.Bootstrap.Forms.File
{
	public class FileEntry
	{
		/// <summary>Gets or sets the file name.</summary>
		/// <value>The file name.</value>
		public string Name { get; set; }

		/// <summary>Gets or sets the content type of the file.</summary>
		/// <value>The content type of the file.</value>
		public string Type { get; set; }

		/// <summary>Gets or sets the file byte data.</summary>
		/// <value>Byte array.</value>
		public byte[] Data { get; set; }

		/// <summary>Gets or sets the last modified DateTime.</summary>
		/// <value>The last modified DateTime.</value>
		public DateTime LastModified { get; set; }

		/// <summary>Gets the size of the file in bytes.</summary>
		/// <value>The size.</value>
		public int Size { get => Data?.Length ?? 0; }

		/// <summary>Converts to the byte data to a Base64 encoded Url suitable for use in an Image tag.</summary>
		/// <example>
		/// &lt;img src="@myFile.ToBase64Url"&gt;
		/// </example>
		/// <value>To base64 URL.</value>
		public string ToBase64Url { get => $"data:{Type};base64,{Convert.ToBase64String(Data, Base64FormattingOptions.None)}"; }

		public async Task Import(Stream stream)
		{
			using (MemoryStream memory = new MemoryStream())
			{
				// Copy to Memory
				await stream.CopyToAsync(memory);

				// Convert to Byte Array
				Data = memory.ToArray();
			}
		}
	}
}