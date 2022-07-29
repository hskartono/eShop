using System;

namespace Infrastructure.ServiceDiscovery
{
    public class ServiceConfig
    {
        public Uri ServiceDiscoveryAddress { get; set; }
        public Uri ServiceAddress { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceId { get; set; } = string.Empty;
    }
}
