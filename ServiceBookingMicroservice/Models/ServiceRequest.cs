using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceBooking.Models
{
    public class ServiceRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Productid { get; set; }

        [Required]
        public int Userid { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public string Problem { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public ICollection<ServiceReport> Reports { get; set; }
    }
}
