using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBazar
{
    public class Product
    {
        private int productId;
        private int departmentId;
        private string productName;
        private double price;

        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public bool Exist { get; set; }

        public Product(int givenProductId, string givenName, double givenPrice, string givenDepartmentName)
        {
            ProductId = givenProductId;
            Name = givenName;
            Price = givenPrice;
            DepartmentName = givenDepartmentName;
            
        }
        public Product(int givenId, int givenDId, string givenName, double givenPrice)
        {
            ProductId = givenId;
            DapartmentId = givenDId;
            ProductName = givenName;
            Price = givenPrice;
        }
        public override string ToString()
        {
            return $"Product Id {ProductId}\r\nProduct Id:{Name}\r\nProduct Price:{Price}\r\nDepartment Name:{DepartmentName} ";
        }

        public int ProductId
        {
            get
            {
                return this.productId;
            }
            private set
            {
                this.productId = value;
            }
        }
        public int DapartmentId
        {
            get
            {
                return this.departmentId;
            }
            private set
            {
                this.departmentId = value;
            }
        }
        public string ProductName
        {
            get
            {
                return this.productName;
            }
            private set
            {
                this.productName = value;
            }
        }
        public double Price
        {
            get
            {
                return this.price;
            }
            private set
            {
                this.price = value;
            }
        }
    }
}
