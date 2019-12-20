using System.Collections.Generic;

namespace Event.Models.HttpResponses
{
    public class EventsListResponse
    {
        public EventsListResponse()
        {
            Events = new List<EventsDetailResponse>();
        }

        public List<EventsDetailResponse> Events { get; set; }
        public int PageCount { get; set; }
    }
}