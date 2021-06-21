using System;

namespace BadProject.Interfaces
{
    public interface IErrorHandler
    {
        void AddError(DateTime dateTime);

        int GetErrorCount();
    }
}