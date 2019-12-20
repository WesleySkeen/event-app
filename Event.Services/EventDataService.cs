using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event.Models.HttpResponses;
using Event.Services.External;

namespace Event.Services
{
    public class EventDataService : IEventDataService
    {
        private string _cachekey = "Events";
        
        private readonly ICacheService _cache;
        private readonly IExternalEventService _externalEventService;

        public EventDataService(ICacheService cache, IExternalEventService externalEventService)
        {
            _cache = cache;
            _externalEventService = externalEventService;
        }

        public async Task<List<EventsDetailResponse>> GetEventsAsync()
        {
            return await getEventsFromCacheAsync();
        }
        
        public async Task<EventsDetailResponse> GetEventAsync(string id)
        {
            return await getEventFromCacheAsync(id);
        }
        
        public async Task<IEnumerable<Category>> GetEventCategoriesAsync()
        {
            var events = await getEventsFromCacheAsync();
            
            return events
                .SelectMany(_ => _.Categories)
                .GroupBy(x => x.Id, (key, category) => category.First());
        }

        private async Task<List<EventsDetailResponse>> getEventsFromCacheAsync()
        {
            var events = _cache.Get<List<EventsDetailResponse>>(_cachekey);
            if (events == null)
            {
                events = await _externalEventService.GetEventsAsync();
                
                _cache.Set(_cachekey, events);
            }   
            List<EventsDetailResponse> response = events;
            
            return response;
        }
        
        private async Task<EventsDetailResponse> getEventFromCacheAsync(string id)
        {
            var events = await getEventsFromCacheAsync();
            
            EventsDetailResponse response = events.First(_ => _.Id == id);
            
            return response;
        }
    }

    public interface IEventDataService
    {
        Task<List<EventsDetailResponse>> GetEventsAsync();
        Task<EventsDetailResponse> GetEventAsync(string id);
        Task<IEnumerable<Category>> GetEventCategoriesAsync();
    }
}