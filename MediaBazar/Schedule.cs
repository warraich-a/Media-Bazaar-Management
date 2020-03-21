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
    class Schedule
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
        public int ScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public ShiftStatus Status { get; set; }
        public override string ToString()
        {
            return $"{employeeId}: {dateTime}-{shiftType}-{status}";
        }

    }
}
