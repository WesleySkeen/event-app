using System;

namespace Event.Models.HttpRequests
{
    public class Filter
    {
        public DateFilter? DateFilter { get; set; }
        public StatusFilter? StatusFilter { get; set; }
        public CategoryFilter? CategoryFilter { get; set; }
    }

    public class DateFilter
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class StatusFilter
    {
        public bool IsClosed { get; set; }
    }

    public class CategoryFilter
    {
        public int[] CategoryIds { get; set; }
    }

    public static class FilterTypes
    {
        public static int Date = 1;
        public static int Status = 2;
        public static int Category = 3;
    }
}