using System;

namespace diet_server_api.Exceptions
{
    public class SearchNotFound : Exception
    {
        public SearchNotFound(string message) : base(message)
        {
        }
    }
}