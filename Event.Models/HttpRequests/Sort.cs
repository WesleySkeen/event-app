namespace Event.Models.HttpRequests
{
    public class SortBy
    {
        public SortType SortType { get; set; }
        public SortOrder SortOrder { get; set; }
    }

    public enum SortType
    {
        Date = 1,
        Status = 2,
        Category = 3
    }

    public enum SortOrder
    {
        Asc = 1,
        Desc = 2
    }
}