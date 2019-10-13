using System;

namespace ApplicationA
{
    public static class ApplicationBConnectionSettings
    {
        public static int Port { get; private set; }

        public static string Host { get; private set; }

        public static DateTime LastModifiedDate { get; private set; }

        public static void Update(string host, int port)
        {
            var portMaxValue = 65535;
            var portMinValue = 0;

            if (port >= portMaxValue || port <= portMinValue)
                throw new ArgumentException($"Invalid {nameof(port)} value");

            // todo: check host

            Host = host;
            Port = port;
            LastModifiedDate = DateTime.Now; // todo: use utc
        }
    }
}
