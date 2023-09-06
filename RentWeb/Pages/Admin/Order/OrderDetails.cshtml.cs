using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using Rent.Models.ViewModel;
using Rent.Utility;

namespace RentWeb.Pages.Admin.Order
{
	public class OrderDetailsModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderDetailVM OrderDetailVM { get; set; }

		public OrderDetailsModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void OnGet(int id)
		{
			OrderDetailVM = new()
			{
				OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser"),
				OrderDetails = _unitOfWork.OrderDetail.GetAll(u=>u.OrderId== id).ToList()
			};
		}

		public IActionResult OnPostOrderCompleted(int orderId)
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusCompleted);
			_unitOfWork.Save();
			return RedirectToPage("OrderList");
		}

		public IActionResult OnPostOrderCancel(int orderId)
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusCancelled);
			_unitOfWork.Save();
			return RedirectToPage("OrderList");
		}
	}
}
