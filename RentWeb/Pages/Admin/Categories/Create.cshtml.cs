using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using RentWeb.DataAccess.Data;

namespace RentWeb.Pages.Admin.Categories
{
	[BindProperties]
	public class CreateModel : PageModel
	{
		private readonly IUnitOfWork _untiOfWork;

		public Category Category { get; set; }

		public CreateModel(IUnitOfWork untiOfWork)
		{
			_untiOfWork = untiOfWork;
		}

		public void OnGet(int id)
		{
		}

		public async Task<IActionResult> OnPost()
		{
			if (Category.Name == Category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Category.Name", "ѕор€дковый номер не может быть равен наименованию категории");
			}

			if (ModelState.IsValid)
			{
				_untiOfWork.Category.Add(Category);
				_untiOfWork.Save();
				TempData["success"] = " атегори€ успешно создана";
				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}
