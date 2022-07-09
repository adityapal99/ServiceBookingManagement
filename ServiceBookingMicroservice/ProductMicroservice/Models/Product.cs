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

        [Required(ErrorMessage = "Name Field is missing")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Make Field is missing")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model field is missing")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Cost field is missing")]
        public float Cost { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
