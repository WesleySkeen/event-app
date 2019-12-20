namespace Event.Models.HttpRequests
{
    public class GetEventsRequest
    {
        public SortBy SortBy { get; set; }
        public Filter Filter { get; set; }

        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}