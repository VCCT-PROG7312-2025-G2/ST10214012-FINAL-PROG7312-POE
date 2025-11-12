using System;

namespace TESTER.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";  
        public int Priority { get; set; } = 0;          
        public string Category { get; set; }           
        public string Location { get; set; }
    }
}
