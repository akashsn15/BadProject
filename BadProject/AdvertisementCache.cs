using System;
using System.Runtime.Caching;
using BadProject.Interfaces;
using ThirdParty;

namespace BadProject
{
    public sealed class AdvertisementCache : IAdvertisementCache
    {
        private static AdvertisementCache _instance;
        private static readonly object lockObj = new object();

        private readonly MemoryCache _cache;

        private AdvertisementCache()
        {
            _cache = new MemoryCache("cache");
        }

        public static AdvertisementCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                            _instance = new AdvertisementCache();
                    }
                }

                return _instance;
            }
        }

        public Advertisement Get(string id)
        {
            string key = GetKey(id);
            lock (lockObj)
            {
                return (Advertisement)(_cache.Get(key));
            }
        }

        public void Set(string id, Advertisement advertisement)
        {
            string key = GetKey(id);

            lock (lockObj)
            {
                _cache.Set(key, advertisement, DateTimeOffset.Now.AddMinutes(5));
            }
        }

        private string GetKey(string id)
        {
            return $"AdvKey_{id}";
        }
    }
}
