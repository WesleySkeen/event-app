using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Event.Models.HttpRequests;
using Event.Models.HttpResponses;
using Moq;
using Xunit;

namespace Event.Services.Tests
{
    public class EventServiceTests
    {
        private Mock<IEventDataService> _eventDataService = new Mock<IEventDataService>();
        private EventService _eventService;

        public EventServiceTests()
        {
            _eventDataService.Setup(_ => _.GetEventsAsync())
                .ReturnsAsync(getEvents());

            _eventService = new EventService(_eventDataService.Object);
        }

        [Theory]
        [InlineData(new[] {1}, 3)]
        [InlineData(new[] {2}, 1)]
        [InlineData(new[] {1, 2}, 4)]
        public async Task Get_Correctly_Filtered_Event_Count_By_Category(int[] categoryIds, int expectedCount)
        {
            var events = await _eventService.GetEventsAsync(new GetEventsRequest
            {
                Filter = new Filter
                {
                    CategoryFilter = new CategoryFilter {CategoryIds = categoryIds}
                }
            });

            Assert.Equal(expectedCount, events.Events.Count);
        }

        [Theory]
        [InlineData(true, 2)]
        [InlineData(false, 2)]
        [InlineData(null, 4)]
        public async Task Get_Correctly_Filtered_Event_Count_By_Status(bool? isClosed, int expectedCount)
        {
            EventsListResponse events;
            if (isClosed.HasValue)
            {
                events = await _eventService.GetEventsAsync(new GetEventsRequest
                {
                    Filter = new Filter
                    {
                        StatusFilter = new StatusFilter {IsClosed = isClosed.Value}
                    }
                });
            }
            else
            {
                events = await _eventService.GetEventsAsync(new GetEventsRequest());
            }

            Assert.Equal(expectedCount, events.Events.Count);
        }

        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(2, 1, 2)]
        [InlineData(3, 0, 3)]
        [InlineData(3, 1, 1)]
        [InlineData(4, 0, 4)]
        [InlineData(4, 1, 0)]
        [InlineData(5, 0, 4)]
        public async Task Get_Correctly_Paged_Events_Count(int pageSize, int pageNumber, int expectedCount)
        {
            var events = await _eventService.GetEventsAsync(new GetEventsRequest
            {
                Page = pageNumber,
                PageSize = pageSize
            });

            Assert.Equal(expectedCount, events.Events.Count);
        }

        [Fact]
        public async Task Get_Correctly_Filtered_Event_Count_By_Combination_Filter()
        {
            var events = await _eventService.GetEventsAsync(new GetEventsRequest
            {
                Filter = new Filter
                {
                    CategoryFilter = new CategoryFilter{ CategoryIds = new[]{1}}, // 3
                    DateFilter = new DateFilter {To = DateTime.Now.AddDays(-5), From = DateTime.Now.AddDays(-8)}, // 2
                    StatusFilter = new StatusFilter{ IsClosed = true}
                }
            });

            Assert.Single(events.Events);
        }
        
        [Fact]
        public async Task Get_Correctly_Filtered_Event_Count_By_Date()
        {
            var events = await _eventService.GetEventsAsync(new GetEventsRequest
                {
                    Filter = new Filter
                    {
                        DateFilter = new DateFilter {To = DateTime.Now, From = DateTime.Now.AddDays(-20)}
                    }
                }
            );

            Assert.Equal(4, events.Events.Count);
        }
        
        private List<EventsDetailResponse> getEvents()
        {
            return new List<EventsDetailResponse>
            {
                new EventsDetailResponse
                {
                    Title = "Event 1",
                    Categories = new Category[1]
                    {
                        new Category
                        {
                            Id = 1,
                            Title = "Category A"
                        }
                    },
                    Geometries = new Geometry[1]
                    {
                        new Geometry
                        {
                            Date = DateTime.Now.AddDays(-2),
                            Type = "Point"
                        }
                    }
                },
                new EventsDetailResponse
                {
                    Title = "Event 2",
                    Closed = DateTime.Now,
                    Categories = new Category[1]
                    {
                        new Category
                        {
                            Id = 2,
                            Title = "Category B"
                        }
                    },
                    Geometries = new Geometry[1]
                    {
                        new Geometry
                        {
                            Date = DateTime.Now.AddDays(-7),
                            Type = "Point"
                        }
                    }
                },
                new EventsDetailResponse
                {
                    Title = "Event 3",
                    Closed = DateTime.Now,
                    Categories = new Category[1]
                    {
                        new Category
                        {
                            Id = 1,
                            Title = "Category A"
                        }
                    },
                    Geometries = new Geometry[1]
                    {
                        new Geometry
                        {
                            Date = DateTime.Now.AddDays(-7),
                            Type = "Point"
                        }
                    }
                },
                new EventsDetailResponse
                {
                    Title = "Event 4",
                    Categories = new Category[1]
                    {
                        new Category
                        {
                            Id = 1,
                            Title = "Category A"
                        }
                    },
                    Geometries = new Geometry[1]
                    {
                        new Geometry
                        {
                            Date = DateTime.Now.AddDays(-7),
                            Type = "Point"
                        }
                    }
                }
            };
        }
    }
}