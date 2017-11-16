using EmployeeDirectory.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeDirectory.Helper
{
    public class GoogleCalendarManager
    {
        private static string calID = SessionManager.UserEmail; //System.Configuration.ConfigurationManager.AppSettings["GoogleCalendarID"].ToString()
        private static string UserId = SessionManager.UserEmail; //System.Web.HttpContext.Current.User.Identity.Name
        private static string gFolder = System.Web.HttpContext.Current.Server.MapPath("/GoogleJson");

        public static CalendarService GetCalendarService(GoogleTokenModel GoogleTokenModelObj)
        {
            CalendarService service = null;

            IAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = GetClientConfiguration().Secrets,
                    DataStore = new FileDataStore(gFolder),
                    Scopes = new[] { CalendarService.Scope.Calendar }
                });

            var uri = /*"http://localhost:19594/GoogleCalendarRegistration.aspx";*/System.Web.HttpContext.Current.Request.Url.ToString();
            var code = System.Web.HttpContext.Current.Request["code"];
            if (code != null)
            {
                var token = flow.ExchangeCodeForTokenAsync(UserId, code,
                    uri.Substring(0, uri.IndexOf("?")), CancellationToken.None).Result;

                // Extract the right state.
                var oauthState = AuthWebUtility.ExtracRedirectFromState(
                    flow.DataStore, UserId, System.Web.HttpContext.Current.Request["state"]).Result;
                System.Web.HttpContext.Current.Response.Redirect(oauthState);
            }
            else
            {
                var result = new AuthorizationCodeWebApp(flow, uri, uri).AuthorizeAsync(UserId, CancellationToken.None).Result;
                if (result.RedirectUri != null)
                {
                    // Redirect the user to the authorization server.
                    System.Web.HttpContext.Current.Response.Redirect(result.RedirectUri);
                    //var page = System.Web.HttpContext.Current.CurrentHandler as Page;
                    //page.ClientScript.RegisterClientScriptBlock(page.GetType(),
                    //    "RedirectToGoogleScript", "window.top.location = '" + result.RedirectUri + "'", true);
                }
                else
                {
                    // The data store contains the user credential, so the user has been already authenticated.
                    service = new CalendarService(new BaseClientService.Initializer
                    {
                        ApplicationName = "My ASP.NET Google Calendar App",
                        HttpClientInitializer = result.Credential
                    });
                }
            }

            return service;
        }

        public static GoogleClientSecrets GetClientConfiguration()
        {
            using (var stream = new FileStream(gFolder + @"\client_secret_new.json", FileMode.Open, FileAccess.Read))
            {
                return GoogleClientSecrets.Load(stream);
            }
        }
        public static CalendarService GetService(string apiKey)
        {
            try
            {
                if (string.IsNullOrEmpty(apiKey))
                    throw new ArgumentNullException("api Key");

                return new CalendarService(new BaseClientService.Initializer()
                {
                    ApiKey = apiKey,
                    ApplicationName = string.Format("{0} API key example", System.Diagnostics.Process.GetCurrentProcess().ProcessName),
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create new Calendar Service", ex);
            }
        }

        public static bool AddEvent()
        {
            CalendarService calService = GetService("118074046336811904856");
            EventsResource er = new EventsResource(calService);
            string ExpKey = "EventID";
            string ExpVal = "1";

            //var queryEvent = er.List(calID);
            //queryEvent.SharedExtendedProperty = ExpKey + "=" + ExpVal; //"EventID=9999"
            //var EventsList = queryEvent.Execute();


            Event newEvent = new Event()
            {
                Summary = "Google I/O 2015",
                Location = "800 Howard St., San Francisco, CA 94103",
                Description = "A chance to hear more about Google's developer products.",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2017-10-20T09:00:00-07:00"),
                    TimeZone = "America/Los_Angeles",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2017-10-20T17:00:00-07:00"),
                    TimeZone = "America/Los_Angeles",
                },
                Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                Attendees = new EventAttendee[] {
                                //new EventAttendee() { Email = "30l@hcsgops.com" },
                                new EventAttendee() { Email = "30L@hcsgops.com" },
                                //new EventAttendee() { Email = "nipe@hcsgcorp.com" },
                                //new EventAttendee() { Email = "mtest1@hcsgcorp.com" },
                                //new EventAttendee() { Email = "ipenibu@gmail.com" },
                            },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] {
                                    new EventReminder() { Method = "email", Minutes = 24 * 60 },
                                    new EventReminder() { Method = "sms", Minutes = 10 },
                                }
                }
            };

            er.Insert(newEvent, calID).Execute();
            return true;
        }
        public static bool AddUpdateDeleteEvent(List<GoogleCalendarAppointmentModel> GoogleCalendarAppointmentModelList, List<GoogleTokenModel> GoogleTokenModelList, double TimeOffset, UserCredential credential)
        {
            //Get the calendar service for a user to add/update/delete events
            //CalendarService calService = GetCalendarService(GoogleTokenModelList[0]);
            CalendarService calService = GetService("1d7cc80dfbdad59263429410f75a9e1768fd2735");
            //var initializer = new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = "ApolloWebApp",
            //};
            //var calService = new CalendarService(initializer);

            if (GoogleCalendarAppointmentModelList != null && GoogleCalendarAppointmentModelList.Count > 0)
            {
                foreach (GoogleCalendarAppointmentModel GoogleCalendarAppointmentModelObj in GoogleCalendarAppointmentModelList)
                {
                    EventsResource er = new EventsResource(calService);
                    string ExpKey = "EventID";
                    string ExpVal = GoogleCalendarAppointmentModelObj.EventID;

                    var queryEvent = er.List(calID);
                    queryEvent.SharedExtendedProperty = ExpKey + "=" + ExpVal; //"EventID=9999"
                    var EventsList = queryEvent.Execute();

                    //to restrict the appointment for specific staff only
                    //Delete this appointment from google calendar
                    if (GoogleCalendarAppointmentModelObj.DeleteAppointment == true)
                    {
                        string FoundEventID = String.Empty;
                        foreach (Event evItem in EventsList.Items)
                        {
                            FoundEventID = evItem.Id;
                            if (!String.IsNullOrEmpty(FoundEventID))
                            {
                                er.Delete(calID, FoundEventID).Execute();
                            }
                        }
                        return true;
                    }
                    //Add if not found OR update if appointment already present on google calendar
                    else
                    {
                        Event newEvent = new Event()
                        {
                            Summary = "Google I/O 2015",
                            Location = "800 Howard St., San Francisco, CA 94103",
                            Description = "A chance to hear more about Google's developer products.",
                            Start = new EventDateTime()
                            {
                                DateTime = DateTime.Parse("2017-10-05T09:00:00-07:00"),
                                TimeZone = "America/Los_Angeles",
                            },
                            End = new EventDateTime()
                            {
                                DateTime = DateTime.Parse("2017-10-05T17:00:00-07:00"),
                                TimeZone = "America/Los_Angeles",
                            },
                            Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                            Attendees = new EventAttendee[] {
                                //new EventAttendee() { Email = "30l@hcsgops.com" },
                                new EventAttendee() { Email = "hiruit.1988@gmail.com" },
                                //new EventAttendee() { Email = "nipe@hcsgcorp.com" },
                                //new EventAttendee() { Email = "mtest1@hcsgcorp.com" },
                                //new EventAttendee() { Email = "ipenibu@gmail.com" },
                            },
                            Reminders = new Event.RemindersData()
                            {
                                UseDefault = false,
                                Overrides = new EventReminder[] {
                                    new EventReminder() { Method = "email", Minutes = 24 * 60 },
                                    new EventReminder() { Method = "sms", Minutes = 10 },
                                }
                            }
                        };

                        er.Insert(newEvent, calID).Execute();



                        //Event eventEntry = new Event();

                        //EventDateTime StartDate = new EventDateTime();
                        //EventDateTime EndDate = new EventDateTime();
                        //StartDate.Date = GoogleCalendarAppointmentModelObj.EventStartTime.ToString("yyyy-MM-dd"); //"2014-11-17";
                        //EndDate.Date = StartDate.Date; //GoogleCalendarAppointmentModelObj.EventEndTime

                        ////Always append Extended Property whether creating or updating event
                        //Event.ExtendedPropertiesData exp = new Event.ExtendedPropertiesData();
                        //exp.Shared = new Dictionary<string, string>();
                        //exp.Shared.Add(ExpKey, ExpVal);

                        //eventEntry.Summary = GoogleCalendarAppointmentModelObj.EventTitle;
                        //eventEntry.Start = StartDate;
                        //eventEntry.End = EndDate;
                        //eventEntry.Location = GoogleCalendarAppointmentModelObj.EventLocation;
                        //eventEntry.Description = GoogleCalendarAppointmentModelObj.EventDetails;
                        //eventEntry.ExtendedProperties = exp;
                        ////EventAttendee[] Attendees = new EventAttendee[] {
                        ////          new EventAttendee() { Email = "lpage@example.com" },
                        ////          new EventAttendee() { Email = "sbrin@example.com" },
                        ////        };
                        //string FoundEventID = String.Empty;
                        //foreach (var evItem in EventsList.Items)
                        //{
                        //    FoundEventID = evItem.Id;
                        //    if (!String.IsNullOrEmpty(FoundEventID))
                        //    {
                        //        //Update the event
                        //        er.Update(eventEntry, calID, FoundEventID).Execute();
                        //    }
                        //}

                        //if (String.IsNullOrEmpty(FoundEventID))
                        //{
                        //    //create the event
                        //    er.Insert(eventEntry, calID).Execute();
                        //}

                        return true;
                    }
                }
            }

            return false;
        }

    }
}