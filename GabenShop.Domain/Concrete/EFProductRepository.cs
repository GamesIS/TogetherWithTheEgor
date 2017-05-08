using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GabenShop.Domain.Entities;
using GabenShop.Domain.Abstract;

namespace GabenShop.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products;  }
        }
    }
}
