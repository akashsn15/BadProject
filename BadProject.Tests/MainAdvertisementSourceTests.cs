using BadProject.Interfaces;
using Moq;
using NUnit.Framework;
using ThirdParty;

namespace BadProject.Tests
{
    public class MainAdvertisementSourceTests
    {
        [Test]
        public void GetAdvertisement_10Errors_ShouldReturnNull()
        {
            var cacheMock = new Mock<IAdvertisementCache>();
            var errorHandlerMock = new Mock<IErrorHandler>();
            errorHandlerMock.Setup(x => x.GetErrorCount())
                .Returns(10);

            var mainAdvertisementSource = new MainAdvertisementSource(cacheMock.Object, errorHandlerMock.Object);
            bool flag = mainAdvertisementSource.GetAdvertisement("Adv1", out Advertisement advertisement);

            Assert.False(flag);
            Assert.Null(advertisement);
        }

        [Test]
        public void GetAdvertisement_NoErrors_ShouldReturnAdvertisement()
        {
            var cacheMock = new Mock<IAdvertisementCache>();
            var errorHandlerMock = new Mock<IErrorHandler>();
            errorHandlerMock.Setup(x => x.GetErrorCount())
                .Returns(0);

            var mainAdvertisementSource = new MainAdvertisementSource(cacheMock.Object, errorHandlerMock.Object);
            bool flag = mainAdvertisementSource.GetAdvertisement("Adv1", out Advertisement advertisement);

            Assert.True(flag);
            Assert.NotNull(advertisement);
        }
    }
}