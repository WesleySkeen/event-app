using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event.Models.HttpResponses;
using Event.Services.External;
using Moq;
using Xunit;

namespace Event.Services.Tests
{
    public class EventDataServiceTests
    {
        private string _eventFromCacheTitle = "Event From Cache";
        private string _eventFromWebTitle = "Event From Web";
        
        private Mock<IExternalEventService> _externalEventService = new Mock<IExternalEventService>();
        private Mock<ICacheService> _cache = new Mock<ICacheService>();
        
        [Fact]
        public async Task Get_Returns_An_Event_When_Cache_Has_Events()
        {
            setCacheItem();
            var eventDataService = new EventDataService(_cache.Object, _externalEventService.Object);
            
            var events = await eventDataService.GetEventsAsync();

            Assert.Single(events);
            Assert.Equal(_eventFromCacheTitle, events.ElementAt(0).Title);
        }
        
        [Fact]
        public async Task Get_Returns_An_Event_From_Web_When_Cache_Has_No_Events()
        {
            setWebItem();
            var eventDataService = new EventDataService(_cache.Object, _externalEventService.Object);
            
            var events = await eventDataService.GetEventsAsync();

            Assert.Single(events);
            Assert.Equal(_eventFromWebTitle, events.ElementAt(0).Title);
        }

        private EventsDetailResponse getEvent(string title)
        {
            return
                new EventsDetailResponse
                {
                    Title = title
                };
        }

        private void setCacheItem()
        {
            var events = new List<EventsDetailResponse>();
            events.Add(getEvent(_eventFromCacheTitle));
            
            _cache.Setup(_ => _.Get<List<EventsDetailResponse>>(It.IsAny<string>())).Returns(events);
        }
        
        private void setWebItem()
        {
            var response = new List<EventsDetailResponse>();
            response.Add(getEvent(_eventFromWebTitle));
            
            _externalEventService.Setup(_ => _.GetEventsAsync()).ReturnsAsync(response);
        }
    }
}