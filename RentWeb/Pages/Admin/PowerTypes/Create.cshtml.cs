using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using RentWeb.DataAccess.Data;

namespace RentWeb.Pages.Admin.PowerTypes
{
	[BindProperties]
	public class CreateModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;

		public PowerType PowerType { get; set; }

		public CreateModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void OnGet(int id)
		{
		}

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.PowerType.Add(PowerType);
				_unitOfWork.Save();
				TempData["success"] = "Источник питания успешно создан";
				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}
