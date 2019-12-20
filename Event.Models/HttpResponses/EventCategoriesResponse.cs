using System.Collections.Generic;

namespace Event.Models.HttpResponses
{
    public class EventCategoriesResponse
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}