using ThirdParty;

namespace BadProject.Interfaces
{
    public interface IAdvertisementCache
    {
        Advertisement Get(string id);

        void Set(string id, Advertisement advertisement);
    }
}
