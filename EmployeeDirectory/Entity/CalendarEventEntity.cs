using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Entity
{
    public class CalendarEventEntity
    {
        public int ID { get; set; }

        public string EventTitle { get; set; }

        public DateTime EventStartTime { get; set; }

        public DateTime EventEndTime { get; set; }

        public string EventLocation { get; set; }

        public string EventDetails { get; set; }

        public string[] EventAttendee { get; set; }

        public List<SelectListItem> EventAttendeeList { get; set; }
    }


}