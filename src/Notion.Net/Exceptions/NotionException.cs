using System;

namespace Notion.Net.Exceptions
{
    public sealed class NotionException : Exception
    {
        public NotionException() : base()
        {

        }

        public NotionException(string message) : base(message)
        {

        }

        public NotionException(string message, Exception innerException) : base(message,innerException)
        {

        }
    }
}
