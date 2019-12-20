using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event.Models.HttpRequests;
using Event.Models.HttpResponses;
using Event.Models.Extensions;

namespace Event.Services
{
    public class EventService : IEventService
    {
        private readonly IEventDataService _eventDataService;

        public EventService(IEventDataService eventDataService)
        {
            _eventDataService = eventDataService;
        }

        public async Task<EventsListResponse> GetEventsAsync(GetEventsRequest request)
        {
            var result  = await _eventDataService.GetEventsAsync();
            
            var filteredEvents = result
                .Filter(request.Filter)
                .SortBy(request.SortBy);

            var eventCount = filteredEvents.Count();
            
            return new EventsListResponse
            {
                Events = filteredEvents.Paginate(request.Page, request.PageSize).ToList(),
                PageCount = (eventCount + request.PageSize - 1) / request.PageSize
            };
        }
        
        public async Task<EventsDetailResponse> GetEventAsync(GetEventRequest request)
        {
            return await _eventDataService.GetEventAsync(request.Id);
        }

        public async Task<IEnumerable<Category>> GetEventCategoriesAsync()
        {
            return await _eventDataService.GetEventCategoriesAsync();
        }
    }

    public interface IEventService
    {
        Task<EventsListResponse> GetEventsAsync(GetEventsRequest request);
        Task<EventsDetailResponse> GetEventAsync(GetEventRequest request);
        Task<IEnumerable<Category>> GetEventCategoriesAsync();
    }
}