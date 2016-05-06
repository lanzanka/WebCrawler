using Core.Crawler;
using Core.Helpers;
using Ninject.Modules;

namespace Example
{
	public class Bindings : NinjectModule
	{
		public override void Load()
		{
			Bind<IUriHelper>().To<UriHelper>().InSingletonScope();
			Bind<ICacheHelper>().To<CacheHelper>().InSingletonScope();
			Bind<IUriList>().To<UriList>().InSingletonScope();
			Bind<IResultOutput>().To<CsvOutput>().InSingletonScope();
		}
	}
}

