using Core.DTOs;

namespace Core.Crawler
{
	public interface IResultOutput
	{
		void SavePageData(PageData pageData);
	}
}