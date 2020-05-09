using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBazar
{
    public class Request
    {
        private int id;
        private int productId;
        private int quantity;
        private string status;
        private string requestedBy;

        public Request(int givenId, int givenPId, int givenquantity, string givenStatus, string givenRBy)
        {
            Id = givenId;
            ProductId = givenPId;
            Quantity = givenquantity;
            Status = givenStatus;
            RequestedBy = givenRBy;
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
        public string RequestedBy
        {
            get
            {
                return this.requestedBy;
            }
            private set
            {
                this.requestedBy = value;
            }
        }
        public string Status
        {
            get
            {
                return this.status;
            }
            private set
            {
                this.status = value;
            }
        }
    }
}
