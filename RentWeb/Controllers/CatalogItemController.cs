using Microsoft.AspNetCore.Mvc;
using Rent.DataAccess.Repository.IRepository;

namespace RentWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CatalogItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var catalogItemList = _unitOfWork.CatalogItem.GetAll(includeProperties: "Category,PowerType");
            return Json(new { data = catalogItemList });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CatalogItem.GetFirstOrDefault(u=> u.Id == id);
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, objFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);

            }
            _unitOfWork.CatalogItem.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful"});
        }
    }
}
