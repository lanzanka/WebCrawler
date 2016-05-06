using System.Runtime.Caching;

namespace Core.Helpers
{
	public class CacheHelper : ICacheHelper
	{
		public virtual bool IsInCache(string key, object value)
		{
			var cacheItemPolicy = new CacheItemPolicy();
			return MemoryCache.Default.AddOrGetExisting(key, value, cacheItemPolicy) != null;
		}
	}
}

