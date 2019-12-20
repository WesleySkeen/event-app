using System.Collections.Generic;
using System.Threading.Tasks;
using Event.Models.HttpRequests;
using Event.Models.HttpResponses;
using Event.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Event.Web.Api.Controllers
{
    [Route("api/1.0/[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<EventsListResponse> Get([FromBody]GetEventsRequest request)
        {
            return await _eventService.GetEventsAsync(request);;
        }
        
        [HttpGet("{id}")]
        public async Task<EventsDetailResponse> Get(string id)
        {
            return await _eventService.GetEventAsync(new GetEventRequest(id));
        }
        
        [HttpGet("GetEventCategories")]
        public async Task<IEnumerable<Category>> GetEventCategories()
        {
            return await _eventService.GetEventCategoriesAsync();
        }
    }
}