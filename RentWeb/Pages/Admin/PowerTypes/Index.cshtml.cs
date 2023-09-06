using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using RentWeb.DataAccess.Data;

namespace RentWeb.Pages.Admin.PowerTypes
{
    public class IndexModel : PageModel
    {
		private readonly IUnitOfWork _unitOfWork;

		public IEnumerable<PowerType> PowerTypes { get; set; }

		public IndexModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public void OnGet()
        {
			PowerTypes = _unitOfWork.PowerType.GetAll();
        }
    }
}
