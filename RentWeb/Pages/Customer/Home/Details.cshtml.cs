using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RentWeb.Pages.Customer.Home
{
	[Authorize]
	public class DetailsModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;

		public DetailsModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[BindProperty]
		public ShoppingCart ShoppingCart { get; set; }


		public void OnGet(int id)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCart = new()
			{
				ApplicationUserId = claim.Value,
				CatalogItem = _unitOfWork.CatalogItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,PowerType"),
				CatalogItemId = id
			};

		}

		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
					filter: u => u.ApplicationUserId == ShoppingCart.ApplicationUserId &&
					u.CatalogItemId == ShoppingCart.CatalogItemId);
				if (shoppingCartFromDb == null)
				{
                    _unitOfWork.ShoppingCart.Add(ShoppingCart);
                    _unitOfWork.Save();
                }
				else
				{
					_unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCart.Count);
				}
				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}
