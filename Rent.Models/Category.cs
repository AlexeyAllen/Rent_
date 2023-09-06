using System.ComponentModel.DataAnnotations;

namespace Rent.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Порядок показа заказа")]
        [Range(0, 100, ErrorMessage = "Должно быть в пределах 1 - 100")]
        public int DisplayOrder { get; set; }
    }
}
