using HtmlAgilityPack;
using System.Collections.Generic;

namespace Core.Extensions
{
	public static class HtmlParser
	{
		public static List<string> GetAllHrefs(this HtmlDocument htmlDocument)
		{
			var hrefs = new List<string>();
			foreach (var link in htmlDocument.DocumentNode.SelectNodes("//a[@href]"))
			{
				var attribute = link.Attributes["href"];
				if(attribute != null && !attribute.Value.StartsWith("javascript"))
				{
					hrefs.Add(attribute.Value);
				}
			}
			return hrefs;
		}

		public static string GetPageTitle(this HtmlDocument htmlDocument)
		{
			var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//head/title");
			return titleNode != null ? titleNode.InnerText : "n/a";
		}

	    public static int GetImagesCount(this HtmlDocument htmlDocument)
	    {
	        var selectedNodes = htmlDocument.DocumentNode.SelectNodes("//img");
	        return selectedNodes != null ? selectedNodes.Count : 0;
	    }
	}
}

