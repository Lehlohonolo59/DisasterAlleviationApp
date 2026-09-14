using System;
using System.Collections.Generic;

namespace DisasterAlleviationApp.Models
{
    // Root object matching the outer structure returned by the NASA EONET API
    public class DisasterApiResponse
    {
        public string Title { get; set; }
        public List<DisasterEvent> Events { get; set; }
    }

    // Represents an individual natural disaster incident (wildfire, flood, earthquake, etc.)
    public class DisasterEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<EventCategory> Categories { get; set; }
        public List<EventSource> Sources { get; set; }
    }

    public class EventCategory
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class EventSource
    {
        public string Id { get; set; }
        public string Url { get; set; }
    }
}