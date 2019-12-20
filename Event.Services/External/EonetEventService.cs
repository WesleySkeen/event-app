using System.Collections.Generic;
using System.Threading.Tasks;
using Event.Models.Configuration;
using Event.Models.HttpResponses;

namespace Event.Services.External
{
    public class EonetEventService : IExternalEventService
    {
        private readonly IHttpEndpointConfigurationSettings _httpEndpointConfigurationSettings;
        private readonly IHttpService _httpService;

        public EonetEventService(IHttpEndpointConfigurationSettings httpEndpointConfigurationSettings, IHttpService httpService)
        {
            _httpEndpointConfigurationSettings = httpEndpointConfigurationSettings;
            _httpService = httpService;
        }
        
        public async Task<List<EventsDetailResponse>> GetEventsAsync()
        {
            var events = new List<EventsDetailResponse>();
            
            var closedEvents = await getEventsFromWebAsync("?status=closed");
            var openEvents = await getEventsFromWebAsync("?status=open");
                
            events.AddRange(closedEvents);
            events.AddRange(openEvents);

            return events;
        }
        
        private async Task<List<EventsDetailResponse>> getEventsFromWebAsync(string queryString)
        {
            var uri = $"{_httpEndpointConfigurationSettings.BaseUri}{queryString}";
            
            var response = await _httpService.GetAsync<EventsListResponse>(uri);
            
            return response.Events;
        }
    }

    public interface IExternalEventService
    {
        Task<List<EventsDetailResponse>> GetEventsAsync();
    }
}