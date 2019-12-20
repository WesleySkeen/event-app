using System;
using System.Collections.Generic;
using System.Linq;
using Event.Models.HttpRequests;
using Event.Models.HttpResponses;

namespace Event.Models.Extensions
{
    public static class EventsListExtensions
    {
        public static List<EventsDetailResponse> Filter(this List<EventsDetailResponse> eventList, Filter filter)
        {
            var statusFilter = filter?.StatusFilter;
            var dateFilter = filter?.DateFilter;
            var categoryFilter = filter?.CategoryFilter;
            var filteredEventList = new List<EventsDetailResponse>();
            
            if (statusFilter != null)
            {
                if(statusFilter.IsClosed)
                    filteredEventList.AddRange(eventList.Where(_ => _.Closed != null));
                else
                    filteredEventList.AddRange(eventList.Where(_ => _.Closed == null));
            }
            else
            {
                filteredEventList = eventList;
            }

            if (dateFilter != null)
                filteredEventList = filteredEventList.Where(_ => _.Geometries.Any(o => o.Date >= dateFilter.From && o.Date <= dateFilter.To.AddDays(1))).ToList();

            if (categoryFilter != null)
                filteredEventList = filteredEventList.Where(_ => _.Categories.Any(o => categoryFilter.CategoryIds.Contains(o.Id))).ToList();
            
            return filteredEventList;
        }
        
        public static List<EventsDetailResponse> SortBy(this List<EventsDetailResponse> eventList, SortBy sortBy)
        {
            if (sortBy != null)
            {
                var sortedEventList = new List<EventsDetailResponse>();
                
                switch (sortBy.SortType)
                {
                    case SortType.Status:
                    {
                        if (sortBy.SortOrder == SortOrder.Desc)
                            sortedEventList = eventList.OrderByDescending(_ => _.Closed).ToList();
                        else
                            sortedEventList = eventList.OrderBy(_ => _.Closed ?? DateTime.MinValue).ToList();
                        break;
                    }
                    case SortType.Date:
                    {
                        if (sortBy.SortOrder == SortOrder.Desc)
                            sortedEventList = eventList.OrderByDescending(_ => _.Geometries.Max(x=>x.Date)).ToList();
                        else
                            sortedEventList = eventList.OrderBy(_ => _.Geometries.Min(x=>x.Date)).ToList();
                        break;
                    }
                    case SortType.Category:
                    {
                        if (sortBy.SortOrder == SortOrder.Desc)
                            sortedEventList = eventList.OrderByDescending(_ => _.Categories.Max(x=>x.Title)).ToList();
                        else
                            sortedEventList = eventList.OrderBy(_ => _.Categories.Min(x=>x.Title)).ToList();
                        break;
                    }
                }

                return sortedEventList;
            }
            return eventList;
        }
        
        public static List<EventsDetailResponse> Paginate(this List<EventsDetailResponse> eventList, int page, int pageSize)
        {
            return eventList.Skip(pageSize * page).Take(pageSize).ToList();;
        }
    }
}