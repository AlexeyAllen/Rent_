using Rent.DataAccess.Repository.IRepository;
using RentWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
			PowerType = new PowerTypeRepository(_db);
			CatalogItem = new CatalogItemRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUser= new ApplicationUserRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepositiory(_db);
		}

		public ICategoryRepository Category { get; private set; }
		public IPowerTypeRepository PowerType { get; private set; }
		public ICatalogItemRepository CatalogItem { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }

		public IOrderHeaderRepositiory OrderHeader { get; private set; }
		public IOrderDetailRepository OrderDetail { get; private set; }
		public IApplicationUserRepository ApplicationUser { get; private set; }


		public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
