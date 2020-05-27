using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBazar
{
    public class Stock
    {

        private int id;
        private int productId;

        private int quantity;

        public Stock(int givenId, int givenQuantity, int givenPId)
        {
            id = givenId;
            productId = givenPId;
            Quantity = givenQuantity;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            private set
            {
                this.id = value;
            }
        }



        public int Quantity
        {
            get
            {
                return this.quantity;
            }
            private set
            {
                this.quantity = value;
            }
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

    }
}


