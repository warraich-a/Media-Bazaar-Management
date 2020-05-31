using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBazar
{
    public class Department
    {
        private int id;
        private string name;
        private int personId;
        private int minEmp;
        public Department(int givenid, string givenName, int givenPId, int givenMinEmp)
        {
            Id = givenid;
            Name = givenName;
            PersonId = givenPId;
            MinEmp = givenMinEmp;
        }

        public Department(string givenName, int givenDepartmentId)
        {
            Id = givenDepartmentId;
            Name = givenName;
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
        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                this.name = value;
            }
        }
        public int PersonId
        {
            get
            {
                return this.personId;
            }
            private set
            {
                this.personId = value;
            }
        }
        public int MinEmp
        {
            get
            {
                return this.minEmp;
            }
            private set
            {
                this.minEmp = value;
            }
        }
    }
}
