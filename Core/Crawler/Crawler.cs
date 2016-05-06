using Core.DTOs;
using Core.Extensions;
using Core.Helpers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Crawler
{
    public class Crawler
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IUriHelper uriHelper;
        private readonly IUriList uriList;
        private readonly IResultOutput resultOutput;

        public int MaxDepth { get; set; }
        
        public Crawler(IUriHelper uriHelper, IUriList uriList, IResultOutput resultOutput)
        {
            this.uriHelper = uriHelper;
            this.uriList = uriList;
            this.resultOutput = resultOutput;

            MaxDepth = 3;
        }

        public async Task Crawl(Uri uri, int depth = 0)
        {
            using (var httpClient = new HttpClient { BaseAddress = uri, Timeout = TimeSpan.FromSeconds(5) })
            {
                try
                {
                    var htmlPage = await httpClient.GetStringAsync(uri);

                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(htmlPage);

                    Console.Write(".");

                    if (depth == 0)
                    {
                        uriList.AddUris(new List<Uri>{ uri }, depth);
                    }
                    
                    resultOutput.SavePageData(new PageData
                    {
                        Url = uri.ToString(),
                        Title = htmlDocument.GetPageTitle(),
                        Depth = depth,
                        Date = DateTime.Now,
                        ImagesCount = htmlDocument.GetImagesCount()
                    });

                    if (depth < MaxDepth)
                    {
                        var uris = uriHelper.GetProperUris(uri, htmlDocument.GetAllHrefs());
                        uriList.AddUris(uris, depth + 1);

                        while(!uriList.IsEmpty())
                        {
                            var nextUri = uriList.GetNextUri();
                            await Crawl(nextUri.Item1, nextUri.Item2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }
            }
        }
    }
}