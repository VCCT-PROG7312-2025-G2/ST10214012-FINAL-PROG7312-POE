using System;

namespace TESTER.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }        // Submitted by
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";  // Pending, In Progress, Completed
        public int Priority { get; set; } = 0;           // Higher = urgent
        public string Category { get; set; }            // Optional, for tree grouping
        public string Location { get; set; }
    }
}
