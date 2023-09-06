using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.DataAccess.Repository.IRepository;
using Rent.Utility;

namespace RentWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[Authorize]
		public IActionResult Get(string? status = null)
		{
			var OrderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");

			if (status == "cancelled")
			{
				OrderHeaderList = OrderHeaderList.Where(u => u.Status == StaticDetails.StatusCancelled);
			}
			else
			{
				if (status == "completed")
				{
					OrderHeaderList = OrderHeaderList.Where(u => u.Status == StaticDetails.StatusCompleted);
				}
				else
				{
					if (status == "inProcess")
					{
						OrderHeaderList = OrderHeaderList.Where(u => u.Status == StaticDetails.StatusInProcess);
					}
					else
					{
						if (status == "ready")
						{
							OrderHeaderList = OrderHeaderList.Where(u => u.Status == StaticDetails.StatusReady);
						}
						else
						{
							if (status == "issued")
							{
								OrderHeaderList = OrderHeaderList.Where(u => u.Status == StaticDetails.StatusIssued);
							}
							else
							{
								if (status == "returned")
								{
									OrderHeaderList = OrderHeaderList.Where(u => u.Status == StaticDetails.StatusReturned);
								}
							}
						}
					}
				}
			}
			return Json(new { data = OrderHeaderList });
		}
	}
}
