using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = [];
    }
}
