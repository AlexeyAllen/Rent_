using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;

namespace RentWeb.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CatalogItem> CatalogItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }


        public void OnGet()
        {
            CatalogItemList = _unitOfWork.CatalogItem.GetAll(includeProperties: "Category,PowerType");
            CategoryList = _unitOfWork.Category.GetAll(orderby: u=> u.OrderBy(c=> c.DisplayOrder));
        }
    }
}
