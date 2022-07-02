using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductMicroservice.Models
{
    public class AppProduct
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public float Cost { get; set; }
        public DateTime? CreatedDate { get; set; } = null;

        public AppProduct(string name, string make, string model, float cost)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Make = make ?? throw new ArgumentNullException(nameof(make));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Cost = cost;
        }
    }
}
