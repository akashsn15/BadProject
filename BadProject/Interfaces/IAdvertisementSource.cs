using ThirdParty;

namespace BadProject.Interfaces
{
    public interface IAdvertisementSource
    {
        bool GetAdvertisement(string id, out Advertisement advertisement);
    }
}