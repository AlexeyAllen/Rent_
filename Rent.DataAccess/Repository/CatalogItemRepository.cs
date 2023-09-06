using Rent.DataAccess.Repository.IRepository;
using Rent.Models;
using RentWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.DataAccess.Repository
{
    public class CatalogItemRepository : Repository<CatalogItem>, ICatalogItemRepository
    {
        private readonly ApplicationDbContext _db;

        public CatalogItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CatalogItem obj)
        {
            var objFromDb = _db.CatalogItem.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Description;
            objFromDb.Price = obj.Price;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.PowerTypeId = obj.PowerTypeId;
            if (objFromDb.ImageUrl != null)
            {
                objFromDb.ImageUrl= obj.ImageUrl;
            }
        }
    }
}
