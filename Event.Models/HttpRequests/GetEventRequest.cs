namespace Event.Models.HttpRequests
{
    public class GetEventRequest
    {
        public GetEventRequest(string id)
        {
            Id = id;
        }
        
        public string Id { get; }
    }
}