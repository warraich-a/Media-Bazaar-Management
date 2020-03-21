using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBazar
{
    public class Schedule
    {
        private int sheduleId;
        private int employeeId;
        private Shift shiftType;
        private DateTime dateTime;
        private ShiftStatus status;

        public static int idSeeder;

        public int SheduleId
        {
            get
            {
                return this.sheduleId;
            }
            private set
            {
                this.sheduleId = value;
            }
        }
        public int EmployeeId
        {
            get
            {
                return this.employeeId;
            }
            private set
            {
                this.employeeId = value;
            }
        }
        public Shift ShiftType
        {
            get
            {
                return this.shiftType;
            }
            private set
            {
                this.shiftType = value;
            }
        }
        public DateTime DATETime
        {
            get
            {
                return this.dateTime;
            }
            private set
            {
                this.dateTime = value;
            }
        }
        public ShiftStatus Status
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

        public Schedule(int sheduleID, int givenEmpId, Shift givenShiftType, DateTime givenDate, ShiftStatus givenStatus)
        {
            this.SheduleId = sheduleID;
            this.EmployeeId = givenEmpId;
            this.ShiftType = givenShiftType;
            this.Status = givenStatus;
            this.DATETime = givenDate;
        }
    }
}
