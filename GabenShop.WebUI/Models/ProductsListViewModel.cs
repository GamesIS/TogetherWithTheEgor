using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GabenShop.Domain.Entities;

namespace GabenShop.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}