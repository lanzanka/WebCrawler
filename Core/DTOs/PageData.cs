using System;

namespace Core.DTOs
{
	public class PageData
	{
		public string Url { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public int Depth { get; set; }
	    public int ImagesCount { get; set; }
	}
}