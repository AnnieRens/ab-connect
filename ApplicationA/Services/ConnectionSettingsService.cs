using ApplicationA.RequestHandlers;

namespace ApplicationA.Services
{
    public class ConnectionSettingsService
    {
        public void Update(string host, int port)
        {
            ApplicationBConnectionSettings.Update(host, port);
        }

        public ConnectionSettingsModel Get()
        {
            return new ConnectionSettingsModel
            {
                Host = ApplicationBConnectionSettings.Host,
                Port = ApplicationBConnectionSettings.Port
            };
        }
    }
}
