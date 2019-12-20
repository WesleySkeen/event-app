using System;

namespace Event.Models.HttpResponses
{
    public class EventsDetailResponse
    {    
        public DateTime? Closed { get; set; }
        
        public string Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Link { get; set; }
        
        public Category[] Categories { get; set; }
        
        public Source[] Sources { get; set; }
        
        public Geometry[] Geometries { get; set; }
    }
    
    public class Category
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public bool Selected { get; set; } = true;
    }

    public class Geometry
    {
        public DateTime Date { get; set; }
        
        public string Type { get; set; }
        
        public object[] Coordinates { get; set; }
    }

    public class Source
    {
        public string Id { get; set; }
        
        public string Url { get; set; }
    }
}