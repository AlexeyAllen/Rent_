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
    public class PowerTypeRepository : Repository<PowerType>, IPowerTypeRepository
	{
        private readonly ApplicationDbContext _db;

        public PowerTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PowerType obj)
        {
            var objFromDb = _db.PowerType.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Name = obj.Name;
        }
    }
}
