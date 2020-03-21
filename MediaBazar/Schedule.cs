using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;

namespace MediaBazar
{
    public class Schedule
    {
        private int scheduleId;
        private int employeeId;
        private Shift shiftType;
        private DateTime dateTime;
        private ShiftStatus status;
        private static int idSeeder;

        public Schedule(int employeeId, Shift shiftType, DateTime dateTime)
        {
            this.employeeId = employeeId;
            this.shiftType = shiftType;
            this.dateTime = dateTime;
        }
        public Schedule(int sheduleID, int givenEmpId, Shift givenShiftType, DateTime givenDate, ShiftStatus givenStatus)
        {
            this.SheduleId = sheduleID;
            this.EmployeeId = givenEmpId;
            this.ShiftType = givenShiftType;
            this.Status = givenStatus;
            this.DATETime = givenDate;
        }
        public int SheduleId
        {
            get
            {
                return this.scheduleId;
            }
            private set
            {
                this.scheduleId = value;
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


        public override string ToString()
        {
            return $"{employeeId}: {dateTime}-{shiftType}-{status}";
        }

    }
}
