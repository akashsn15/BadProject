using System;
using NUnit.Framework;

namespace BadProject.Tests
{
    public class ErrorHandlerTests
    {
        [Test]
        public void GetErrorCount_NoErrors_ShouldReturn0()
        {
            var errorHandler = ErrorHandler.Instance;
            var count = errorHandler.GetErrorCount();

            Assert.That(count == 0);
        }

        [Test]
        public void GetErrorCount_2HourErrors_ShouldReturn1()
        {
            var errorHandler = ErrorHandler.Instance;
            errorHandler.AddError(DateTime.Now.AddHours(-1));
            errorHandler.AddError(DateTime.Now.AddMinutes(-30));

            var count = errorHandler.GetErrorCount();

            Assert.That(errorHandler.ErrorQueueCount == 2);
            Assert.That(count == 1);
        }

        [Test]
        public void AddError_Add25Errors_ShouldRetain20()
        {
            var errorHandler = ErrorHandler.Instance;
            
            for (int i = 0; i < 25; i++)
            {
                errorHandler.AddError(DateTime.Now);
            }

            Assert.That(errorHandler.ErrorQueueCount == 20);
        }
    }
}