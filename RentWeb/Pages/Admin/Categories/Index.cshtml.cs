using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using RentWeb.DataAccess.Data;

namespace RentWeb.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _untiOfWork;

        public IEnumerable<Category> Categories { get; set; }

        public IndexModel(IUnitOfWork untiOfWork)
        {
			_untiOfWork = untiOfWork;
        }

        public void OnGet()
        {
            Categories = _untiOfWork.Category.GetAll();
        }
    }
}
