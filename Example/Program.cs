using Core.Crawler;
using Ninject;
using System;
using System.Reflection;

namespace Example
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var crawler = kernel.Get<Crawler>();
            crawler.MaxDepth = 2;

            Console.WriteLine("Hello, let's start crawling!\n");

            //Main is not in the async context so I have to use Wait()
            crawler.Crawl(new Uri("http://straburzynski.pl/")).Wait();

            Console.WriteLine("\n\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
