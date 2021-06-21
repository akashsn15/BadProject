using BadProject.Interfaces;
using ThirdParty;

namespace BadProject
{
    public class AdvertisementService
    {
        // **************************************************************************************************
        // Loads Advertisement information by id
        // from cache or if not possible uses the "mainProvider" or if not possible uses the "backupProvider"
        // **************************************************************************************************
        // Detailed Logic:
        // 
        // 1. Tries to use cache (and retuns the data or goes to STEP2)
        //
        // 2. If the cache is empty it uses the NoSqlDataProvider (mainProvider), 
        //    in case of an error it retries it as many times as needed based on AppSettings
        //    (returns the data if possible or goes to STEP3)
        //
        // 3. If it can't retrive the data or the ErrorCount in the last hour is more than 10, 
        //    it uses the SqlDataProvider (backupProvider)

        private readonly IAdvertisementCache _cache;
        private readonly IAdvertisementSource _mainAdvertisementSource;

        public AdvertisementService()
        {
             _cache = AdvertisementCache.Instance;
             _mainAdvertisementSource = new MainAdvertisementSource(_cache, ErrorHandler.Instance);
        }

        public Advertisement GetAdvertisement(string id)
        {
            Advertisement advertisement;

            if (TryGetCached(id, out advertisement))
                return advertisement;
            if (_mainAdvertisementSource.GetAdvertisement(id, out advertisement))
                return advertisement;

            TryGetFromBackupProvider(id, out advertisement);
            return advertisement;
        }

        private bool TryGetCached(string id, out Advertisement advertisement)
        {
            advertisement = _cache.Get(id);
            return advertisement != null;
        }

        private bool TryGetFromBackupProvider(string id, out Advertisement advertisement)
        {
            advertisement = SQLAdvProvider.GetAdv(id);

            if (advertisement != null)
            {
                _cache.Set(id, advertisement);
                return true;
            }

            return false;
        }
    }
}
