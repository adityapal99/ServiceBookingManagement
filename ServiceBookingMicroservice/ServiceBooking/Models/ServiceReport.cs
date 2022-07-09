using System;

namespace ServiceBooking.Models
{
    public class ServiceReport
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string ServiceType { get; set; }
        public string ActionTaken { get; set; }
        public string DiagnosisDetails { get; set; }
        public string isPaid { get; set; }
        public int VisitFees { get; set; }
        public string RepairDetails { get; set; }

        public int ServiceRequestId { get; set; }
        public ServiceRequest ServiceRequest { get; set; }

    }
}
