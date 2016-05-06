using Core.Helpers;
using System;
using System.Collections.Generic;

namespace Core.Crawler
{
    public class UriList : IUriList
    {
        public Queue<Tuple<Uri, int>> UriQueue { get; set; }

        private readonly ICacheHelper cacheHelper;	

        public UriList(ICacheHelper cacheHelper)
        {
            this.cacheHelper = cacheHelper;

            UriQueue = new Queue<Tuple<Uri, int>>();
        }

        public bool IsEmpty()
        {
            return UriQueue.Count == 0;
        }

        public Tuple<Uri, int> GetNextUri()
        {
            return UriQueue.Dequeue();
        }

        public void AddUris(List<Uri> uris, int depth)
        {
            foreach (var uri in uris)
            {
                AddUri(uri, depth);
            }
        }

        private void AddUri(Uri uri, int depth)
        {
            if (!IsUriProcessed(uri, depth))
            {
                UriQueue.Enqueue(Tuple.Create(uri, depth));
            }
        }

        private bool IsUriProcessed(Uri uri, int depth)
        {
            return cacheHelper.IsInCache(uri.ToString(), uri) || UriQueue.Contains(Tuple.Create(uri, depth));
        }
    }
}

