namespace Core.Helpers
{
	public interface ICacheHelper
	{
		bool IsInCache(string key, object value);
	}
}

