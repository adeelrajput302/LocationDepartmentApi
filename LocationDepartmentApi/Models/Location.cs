using System;

namespace LocationDepartmentApi.Models
{
    public class Location
    {

        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationDescrption { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
