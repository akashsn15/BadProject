using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using BadProject.Interfaces;
using ThirdParty;

namespace BadProject
{
    public class MainAdvertisementSource : IAdvertisementSource
    {
        private readonly IAdvertisementCache _cache;
        private readonly IErrorHandler _errorHandler;
        private readonly NoSqlAdvProvider _dataProvider;

        public MainAdvertisementSource(IAdvertisementCache cache, IErrorHandler errorHandler)
        {
            _cache = cache;
            _errorHandler = errorHandler;
            _dataProvider = new NoSqlAdvProvider();
        }

        public bool GetAdvertisement(string id, out Advertisement advertisement)
        {
            advertisement = null;
            if (_errorHandler.GetErrorCount() < 10)
            {
                int retry = 0;
                do
                {
                    retry++;
                    advertisement = MakeRequest(id);

                } while (advertisement == null && retry < GetRetryCount());


                if (advertisement != null)
                {
                    _cache.Set(id, advertisement);
                    return true;
                }
            }

            return false;
        }

        private static int GetRetryCount()
        {
            return int.Parse(ConfigurationManager.AppSettings["RetryCount"]);
        }

        private Advertisement MakeRequest(string id)
        {
            Advertisement advertisement = null;

            try
            {
                advertisement = _dataProvider.GetAdv(id);
            }
            catch
            {
                _errorHandler.AddError(DateTime.Now);
                Thread.Sleep(1000);
            }

            return advertisement;
        }
    }


}
