using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Models
{
    public class GoogleCalendarAppointmentModel
    {
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
        public bool DeleteAppointment { get; set; }
        public string EventID { get; set; }
        public string EventLocation { get; set; }
        public string EventTitle { get; set; }
        public string EventDetails { get; set; }
        public string CalendarEventAttendee { get; set; }
        //public List<string> CalendarEventAttendee { get; set; }
    }

    //public class CalendarEventAttendee 
    //{
        
    //}
}