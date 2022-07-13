using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceBooking.Models
{
    public class ServiceReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime ReportDate { get; set; }

        [Required]
        public string ServiceType { get; set; }

        [Required]
        public string ActionTaken { get; set; }

        [Required]
        public string DiagnosisDetails { get; set; }

        [Required]
        public string isPaid { get; set; }

        [Required]
        public int VisitFees { get; set; }

        [Required]
        public string RepairDetails { get; set; }

        [Required]
        public int ServiceRequestId { get; set; }
        public ServiceRequest ServiceRequest { get; set; }

    }
}
