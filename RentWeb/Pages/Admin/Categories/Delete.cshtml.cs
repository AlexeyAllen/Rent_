using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using RentWeb.DataAccess.Data;

namespace RentWeb.Pages.Admin.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
	{
		private readonly IUnitOfWork _untiOfWork;

		public Category Category { get; set; }

		public DeleteModel(IUnitOfWork untiOfWork)
		{
			_untiOfWork = untiOfWork;
		}

		public void OnGet(int id)
		{
			Category = _untiOfWork.Category.GetFirstOrDefault(u => u.Id == id);
		}

		public async Task<IActionResult> OnPost()
		{
			var categoryFromDB = _untiOfWork.Category.GetFirstOrDefault(u => u.Id == Category.Id);
			if (categoryFromDB != null)
			{
				_untiOfWork.Category.Remove(categoryFromDB);
				_untiOfWork.Save();
				TempData["success"] = "Категория успешно удалена";
				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}
