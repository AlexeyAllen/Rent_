using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using Rent.Models.ViewModel;
using Rent.Utility;

namespace RentWeb.Pages.Admin.Order
{
    [Authorize(Roles = $"{StaticDetails.AdminRole},{StaticDetails.RepairManRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public List<OrderDetailVM> OrderDetailVM { get; set; }

        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            OrderDetailVM = new();

            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u => 
			u.Status == StaticDetails.StatusConfirmed ||
			u.Status == StaticDetails.StatusInProcess ||
			u.Status == StaticDetails.StatusReady ||
			u.Status == StaticDetails.StatusIssued).ToList();

            foreach (OrderHeader item in orderHeaders)
            {
                OrderDetailVM individaul = new OrderDetailVM()
                {
                    OrderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetail.GetAll(u => u.OrderId == item.Id).ToList()
                };
                OrderDetailVM.Add(individaul);
            }
        }

		public IActionResult OnPostOrderInProcess(int orderId)
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusInProcess);
			_unitOfWork.Save();
			return RedirectToPage("ManageOrder");
		}

		public IActionResult OnPostOrderReady(int orderId)
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusReady);
			_unitOfWork.Save();
			return RedirectToPage("ManageOrder");
		}

		public IActionResult OnPostOrderIssue(int orderId)
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusIssued);
			_unitOfWork.Save();
			return RedirectToPage("ManageOrder");
		}

		public IActionResult OnPostOrderReturn(int orderId)
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusReturned);
			_unitOfWork.Save();
			return RedirectToPage("ManageOrder");
		}

		public IActionResult OnPostOrderCancel(int orderId)
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderId, StaticDetails.StatusCancelled);
			_unitOfWork.Save();
			return RedirectToPage("ManageOrder");
		}
	}
}
