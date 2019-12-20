using Microsoft.Extensions.Options;

namespace Event.Models.Configuration
{
    public class HttpEndpointConfigurationSettings : IHttpEndpointConfigurationSettings
    {
        public string BaseUri { get; set; }

        public HttpEndpointConfigurationSettings(IOptions<HttpEndpointSettings> dbOptions)
        {
            BaseUri = dbOptions.Value.BaseUri;
        }
    }

    public interface IHttpEndpointConfigurationSettings
    {
        string BaseUri { get; set; }
    }

    public class HttpEndpointSettings
    {
        public string BaseUri { get; set; }
    }
}