using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using Rent.Utility;
using System.Security.Claims;

namespace RentWeb.Pages.Customer.Cart
{
	[Authorize]
	[BindProperties]
	public class SummaryModel : PageModel
	{
		public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

		public OrderHeader OrderHeader { get; set; }
		private readonly IUnitOfWork _unitOfWork;

		public SummaryModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			OrderHeader = new OrderHeader();
		}
		public void OnGet()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
					includeProperties: "CatalogItem,CatalogItem.PowerType,CatalogItem.Category");

				foreach (var cartItem in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cartItem.CatalogItem.Price * cartItem.Count);
				}
				ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
				OrderHeader.PickupName = applicationUser.FirstName + " " + applicationUser.LastName;
				OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
			}
		}

		public void OnPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
					includeProperties: "CatalogItem,CatalogItem.PowerType,CatalogItem.Category");

				foreach (var cartItem in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cartItem.CatalogItem.Price * cartItem.Count);
				}

				OrderHeader.Status = StaticDetails.StatusConfirmed;
				OrderHeader.OrderDate = System.DateTime.Now;
				OrderHeader.UserId = claim.Value;
				OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " + 
					OrderHeader.PickUpTime.ToShortTimeString());
				_unitOfWork.OrderHeader.Add(OrderHeader);
				_unitOfWork.Save();

				foreach (var item in ShoppingCartList)
				{
					OrderDetails orderDetails = new()
					{
						CatalogItemId = item.CatalogItemId,
						OrderId = OrderHeader.Id,
						Name = item.CatalogItem.Name,
						Price = item.CatalogItem.Price,
						Count = item.Count
					};
					_unitOfWork.OrderDetail.Add(orderDetails);
				}

				_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
				_unitOfWork.Save();
			}
		}

	}
}
