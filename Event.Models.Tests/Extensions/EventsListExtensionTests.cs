using System;
using System.Collections.Generic;
using System.Linq;
using Event.Models.Extensions;
using Event.Models.HttpRequests;
using Event.Models.HttpResponses;
using Xunit;

namespace Event.Models.Tests.Extensions
{
    public class EventsListExtensionTests
    {
        [Theory]
        [InlineData(1, "Event 1")]
        [InlineData(2, "Event 2")]
        public void Filter_By_Cat_Id_Should_Return_Correct_Event(int categoryFilterId, string expectedTitle)
        {
            var eventList = getEvents();

            var filteredList = eventList.Filter(new Filter{ CategoryFilter = new CategoryFilter{ CategoryIds = new []{categoryFilterId}}});

            Assert.Single(filteredList);
            Assert.Equal(expectedTitle, filteredList.ElementAt(0).Title);
        }
        
        [Theory]
        [InlineData(false, "Event 1")]
        [InlineData(true, "Event 2")]
        public void Filter_By_Status_Should_Return_Correct_Event(bool isClosed, string expectedTitle)
        {
            var eventList = getEvents();

            var filteredList = eventList.Filter(new Filter{ StatusFilter = new StatusFilter{ IsClosed = isClosed}});

            Assert.Single(filteredList);
            Assert.Equal(expectedTitle, filteredList.ElementAt(0).Title);
        }
        
        [Fact]
        public void Filter_By_Date_Should_Return_Correct_Event()
        {
            var eventList = getEvents();

            var filteredList = eventList.Filter(new Filter{ DateFilter = new DateFilter
            {
                From = DateTime.Now.AddDays(-8),
                To = DateTime.Now.AddDays(-6)
            }});

            Assert.Single(filteredList);
            Assert.Equal("Event 2", filteredList.ElementAt(0).Title);
        }
        
        [Theory]
        [InlineData(SortType.Date, SortOrder.Desc, "Event 1")]
        [InlineData(SortType.Date, SortOrder.Asc, "Event 2")]
        [InlineData(SortType.Category, SortOrder.Desc, "Event 2")]
        [InlineData(SortType.Category, SortOrder.Asc, "Event 1")]
        [InlineData(SortType.Status, SortOrder.Desc, "Event 2")]
        [InlineData(SortType.Status, SortOrder.Asc, "Event 1")]
        public void Sort_By_Should_Return_Correctly_Ordered_Events(SortType sortType, SortOrder sortOrder, string expectedFirstTirle)
        {
            var eventList = getEvents();

            var filteredList = eventList.SortBy(new SortBy{ SortType = sortType, SortOrder = sortOrder});

            Assert.Equal(2, filteredList.Count);
            Assert.Equal(expectedFirstTirle, filteredList.ElementAt(0).Title);
        }
        
        [Theory]
        [InlineData(1, 0,"Event 1")]
        [InlineData(1, 1, "Event 2")]
        public void Paging_Should_Return_Correct_Event(int pageSize, int pageNumber, string expectedTitle)
        {
            var eventList = getEvents();

            var filteredList = eventList.Paginate(pageNumber, pageSize);

            Assert.Single(filteredList);
            Assert.Equal(expectedTitle, filteredList.ElementAt(0).Title);
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
                }
            };
        }
    }
}