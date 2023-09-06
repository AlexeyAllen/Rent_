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
    public class OrderHeaderRepositiory : Repository<OrderHeader>, IOrderHeaderRepositiory
	{
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepositiory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
        }

		public void UpdateStatus(int Id, string status)
		{
            var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == Id);
            if (orderFromDb != null)
            {
                orderFromDb.Status = status;
            }
		}
	}
}
