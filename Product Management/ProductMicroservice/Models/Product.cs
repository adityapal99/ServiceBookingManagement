using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductMicroservice
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public float Cost { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
