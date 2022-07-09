using System;
using System.Collections.Generic;

namespace ServiceBooking.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public int Productid { get; set; }
        public int Userid { get; set; }
        public DateTime RequestDate { get; set; }
        public string Problem { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public ICollection<ServiceReport> Reports { get; set; }
    }
}
