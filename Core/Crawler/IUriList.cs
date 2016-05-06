using System;
using System.Collections.Generic;

namespace Core.Crawler
{
	public interface IUriList
	{
		bool IsEmpty();
		Tuple<Uri, int> GetNextUri();
		void AddUris(List<Uri> uris, int depth);
	}
}

