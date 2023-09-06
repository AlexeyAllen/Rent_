using System.ComponentModel.DataAnnotations;

namespace Rent.Models
{
    public class PowerType
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
    }
}
