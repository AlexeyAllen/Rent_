using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Models
{
	public class OrderDetails
	{
		public int Id { get; set; }
		[Required]
		public int OrderId { get; set; }
		[ForeignKey("OrderId")]
		[ValidateNever]
		public OrderHeader OrderHeader { get; set; }
		[Required]
		public int CatalogItemId { get; set; }
		[ForeignKey("CatalogItemId")]
		public virtual CatalogItem CatalogItem { get; set; }
		public int Count { get; set; }
		[Required]
		public double Price { get; set; }
		public string Name { get; set; }
	}
}
