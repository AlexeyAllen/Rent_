using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using RentWeb.DataAccess.Data;

namespace RentWeb.Pages.Admin.PowerTypes
{
    [BindProperties]
    public class EditModel : PageModel
    {
		private readonly IUnitOfWork _unitOfWork;

		public PowerType PowerType { get; set; }

		public EditModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void OnGet(int id)
        {
			PowerType = _unitOfWork.PowerType.GetFirstOrDefault(u=>u.Id==id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
				_unitOfWork.PowerType.Update(PowerType);
				_unitOfWork.Save();
				TempData["success"] = "Источник питания успешно отредактирован";
				return RedirectToPage("Index");
			}
            return Page();
        }
    }
}
