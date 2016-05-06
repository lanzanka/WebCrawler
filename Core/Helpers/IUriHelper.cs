using System;
using System.Collections.Generic;

namespace Core.Helpers
{
	public interface IUriHelper
	{
		List<Uri> GetProperUris(Uri baseUri, List<string> urls);
	    Uri GetProperUri(Uri baseUri, string url);
	}
}

