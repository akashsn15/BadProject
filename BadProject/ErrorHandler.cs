using System;
using System.Collections.Generic;
using System.Linq;
using BadProject.Interfaces;

namespace BadProject
{
    public sealed class ErrorHandler : IErrorHandler
    {
        private static ErrorHandler _instance;
        private static readonly object lockObj = new object();

        private readonly Queue<DateTime> _errors = new Queue<DateTime>();

        private ErrorHandler() { }

        public static ErrorHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if(_instance == null)
                            _instance = new ErrorHandler();
                    }
                }

                return _instance;
            }
        }

        public void AddError(DateTime dateTime)
        {
            lock (lockObj)
            {
                while (_errors.Count >= 20) // Limit Error Queue to last 20 errors
                    _errors.Dequeue();

                _errors.Enqueue(dateTime); // Store HTTP error timestamp              
            }
        }

        public int GetErrorCount()
        {
            lock (lockObj)
                return _errors.Count(error => error > DateTime.Now.AddHours(-1));
        }

        public int ErrorQueueCount => _errors.Count;
    }
}
