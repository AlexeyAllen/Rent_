using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
		IPowerTypeRepository PowerType { get; }
        ICatalogItemRepository CatalogItem { get; }
        IShoppingCartRepository ShoppingCart { get; }

        IOrderHeaderRepositiory OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
		IApplicationUserRepository ApplicationUser { get; }

		void Save();
    }
}
