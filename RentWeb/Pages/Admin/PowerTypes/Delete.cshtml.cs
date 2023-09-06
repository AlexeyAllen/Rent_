using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using RentWeb.DataAccess.Data;

namespace RentWeb.Pages.Admin.PowerTypes
{
	[BindProperties]
	public class DeleteModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;

		public PowerType PowerType { get; set; }

		public DeleteModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void OnGet(int id)
		{
			PowerType = _unitOfWork.PowerType.GetFirstOrDefault(u => u.Id == id);
		}

		public async Task<IActionResult> OnPost()
		{
			var powerTypeFromDB = _unitOfWork.PowerType.GetFirstOrDefault(u => u.Id == PowerType.Id);
			if (powerTypeFromDB != null)
			{
				_unitOfWork.PowerType.Remove(powerTypeFromDB);
				_unitOfWork.Save();
				TempData["success"] = "Источник питания успешно удален";
				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}
