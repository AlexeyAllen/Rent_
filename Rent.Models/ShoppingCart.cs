﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int CatalogItemId { get; set; }
        [ForeignKey("CatalogItemId")]
        [ValidateNever]
        public CatalogItem CatalogItem { get; set; }

        [Range(1, 100, ErrorMessage = "Please select a count between 1 and 100")]
        public int Count { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }
    }
}
