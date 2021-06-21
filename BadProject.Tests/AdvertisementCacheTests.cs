using NUnit.Framework;
using ThirdParty;

namespace BadProject.Tests
{
    [TestFixture]
    public class AdvertisementCacheTests
    {

        [Test]
        public void Get_EmptyCache_ShouldReturnNull()
        {
            var cache = AdvertisementCache.Instance;
            var adv = cache.Get("abc");

            Assert.Null(adv);
        }

        [Test]
        public void Get_ItemExists_ShouldReturnItem()
        {
            var cache = AdvertisementCache.Instance;
            cache.Set("Adv1", new Advertisement
            { 
                WebId = "WebId", 
                Name = "Test Advertisement",
                Description = "Test Description",

            });
            
            var adv = cache.Get("Adv1");

            Assert.That(adv.Name.Equals("Test Advertisement"));
        }

        [Test]
        public void Set_EmptyCache_ShouldReturnAdd()
        {
            var cache = AdvertisementCache.Instance;
            cache.Set("Adv1", new Advertisement
            {
                WebId = "WebId",
                Name = "Test Advertisement",
                Description = "Test Description",

            });

            var adv = cache.Get("Adv1");

            Assert.That(adv.Name.Equals("Test Advertisement"));
        }
    }
}
