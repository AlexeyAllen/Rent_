using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rent.DataAccess.Repository;
using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using RentWeb.DataAccess.Data;

namespace RentWeb.Pages.Admin.CatalogItems
{
	[BindProperties]
	public class UpsertModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;

		private readonly IWebHostEnvironment _hostEnvironment;

		public CatalogItem CatalogItem { get; set; }
		public IEnumerable<SelectListItem> CategoryList { get; set; }
		public IEnumerable<SelectListItem> PowerTypeList { get; set; }


		public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			CatalogItem = new CatalogItem();
			_hostEnvironment = hostEnvironment;
		}

		public void OnGet(int? id)
		{
			if (id != null)
			{
				CatalogItem = _unitOfWork.CatalogItem.GetFirstOrDefault(u => u.Id == id);
			}

			CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem()
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			PowerTypeList = _unitOfWork.PowerType.GetAll().Select(i => new SelectListItem()
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
		}

		public async Task<IActionResult> OnPost()
		{
			string webRootPath = _hostEnvironment.WebRootPath;
			var files = HttpContext.Request.Form.Files;
			if (CatalogItem.Id == 0)
			{
				//create
				string fileName_new = Guid.NewGuid().ToString();
				var uploads = Path.Combine(webRootPath, @"images\catalogitems");
				var extension = Path.GetExtension(files[0].FileName);

				using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
				{
					files[0].CopyTo(fileStream);
				}
				CatalogItem.ImageUrl = @"\images\catalogitems\" + fileName_new + extension;
				_unitOfWork.CatalogItem.Add(CatalogItem);
				_unitOfWork.Save();
			}
			else
			{
				//edit
				var objFromDb = _unitOfWork.CatalogItem.GetFirstOrDefault(u => u.Id == CatalogItem.Id);
				if (files.Count>0)
				{
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\catalogitems");
                    var extension = Path.GetExtension(files[0].FileName);

					//delete old image
					var oldImagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);

                    }
					//new upload
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    CatalogItem.ImageUrl = @"\images\catalogitems\" + fileName_new + extension;

                }
				else
				{
					CatalogItem.ImageUrl = objFromDb.ImageUrl;
				}
				_unitOfWork.CatalogItem.Update(CatalogItem);
				_unitOfWork.Save();
            }
			return RedirectToPage("./Index");
		}
	}
}
