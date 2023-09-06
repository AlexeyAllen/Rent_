using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Models
{
	public class CatalogItem
	{
		[Key]
		public int Id { get; set; }

		[Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Ссылка на изображение")]
        public string ImageUrl { get; set; }

		[Range(1000, 1000000, ErrorMessage ="Цена должна быть между 1000 и 1000000 руб.")]
        [Display(Name = "Стоимость")]
        public double Price { get; set; }

		[Display(Name = "Источник питания")]
		public int PowerTypeId { get; set; }
		[ForeignKey("PowerTypeId")]
		public PowerType PowerType { get; set; }

		[Display(Name = "Категория")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
