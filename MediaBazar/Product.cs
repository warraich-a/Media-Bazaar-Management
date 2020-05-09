using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBazar
    {
        public class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }

            public int ProductId { get; set; }
            public string DepartmentName { get; set; }

            public Product(int givenProductId, string givenName, double givenPrice, string givenDepartmentName)
            {
                ProductId = givenProductId;
                Name = givenName;
                Price = givenPrice;
                DepartmentName = givenDepartmentName;
            }
            public override string ToString()
            {
                return $"Product Id {ProductId}\r\nProduct Id:{Name}\r\nProduct Price:{Price}\r\nDepartment Name:{DepartmentName} ";
            }
        }
}
