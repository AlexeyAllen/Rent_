﻿using Rent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.DataAccess.Repository.IRepository
{
    public interface ICatalogItemRepository : IRepository<CatalogItem>
    {
        void Update(CatalogItem obj);
    }
}
