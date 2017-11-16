using EmployeeDirectory.App_Start;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using EmployeeDirectory.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Controllers
{
    //[RequireHttps]
    //[Authorize]
    public class CalendarController : BaseController
    {
        //
        // GET: /Calendar/
        private static string gFolder = System.Web.HttpContext.Current.Server.MapPath("/GoogleJson");

        public ActionResult EventList()
        {
            return View();
        }

        public ActionResult CreateEvent()
        {
            CalendarEventEntity entity = new CalendarEventEntity();
            List<SelectListItem> NameList = new List<SelectListItem>();
            NameList = new List<SelectListItem>() { new SelectListItem() { Text = "Hiren Patel", Value = "hiruit.1988@gmail.com" } ,
                                                                          new SelectListItem() { Text = "Nibu Ipe (hcsgcorp)", Value = "nipe@hcsgcorp.com" } ,
                                                                          new SelectListItem() { Text = "Nibu Ipe (hcsgops)", Value = "nipe@hcsgops.com" } ,
                                                                          new SelectListItem() { Text = "Nibu Ipe (Personal)", Value = "ipenibu@gmail.com" },
                                                                          new SelectListItem() { Text = "30L", Value = "30l@hcsgops.com" },
                                                                          new SelectListItem() { Text = "mtest1", Value = "mtest1@hcsgcorp.com" }
                                                                    };
            ViewBag.EventAtteneeList = NameList;
            entity.EventAttendeeList = NameList;
            return View(entity);
        }

        public static CalendarService AuthenticateServiceAccount(string serviceAccountEmail, string keyFilePath)
        {

            // check the file exists
            if (!System.IO.File.Exists(keyFilePath))
            {
                Console.WriteLine("An Error occurred - Key file does not exist");
                return null;
            }

            string[] scopes = new string[] {
                CalendarService.Scope.Calendar  ,  // Manage your calendars
                CalendarService.Scope.CalendarReadonly    // View your Calendars
            };

            //var certificate = new X509Certificate2(keyFilePath, "notasecret", X509KeyStorageFlags.Exportable);
            try
            {
                //ServiceAccountCredential
                //    credential = new ServiceAccountCredential(
                //    new ServiceAccountCredential.Initializer(serviceAccountEmail)
                //    {
                //        Scopes = scopes
                //    }.FromCertificate(certificate));

                ServiceAccountCredential credential = new ServiceAccountCredential(
                    new ServiceAccountCredential.Initializer(serviceAccountEmail)
                    {
                        Scopes = scopes
                    }.FromPrivateKey("-----BEGIN PRIVATE KEY-----\nMIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQDXSDyYFy9xrxLC\nZTsMzFgy1kooWifluyidpZFwlmxgmeFGKJ3zBIEb3oxvq+gYzF863lA0nw4BQPE+\nUr/gxpWBUQQGKhQTFof7duI61IZDDamDw4cgPtWNnK/zVpCyOTzulUUrvpviBI0W\nM025h9g+ZLcRexYU0gR7CtakmKEP89LtI5+gjd6y52HlblCjadLdQIqJGX3wp4U2\nkyOPovUFBS2eqUMNZAcz0HlC8eQs/CMGLEjca9QPAMkCll3a5OX9PiN1lfw3rnlY\nO0ZBaQrtGxvjRFiierjiEVaSfvvi4pTwHvne7uHSZzwK8VLi5vuL6D+N7ElLKzwW\nRP6/pHu1AgMBAAECggEAW5kAMyQWSzQW8sBYrhrZ4hN8LQwjEqOd5emW5sUR6s4+\nY/gPMGG3v1BjB3aow7hdQnJpxOHx9wWXP5G801obLZD3edn4faCmDg/otOhNEgWj\nnQ0aFtW1TlIfKRWpbjNdYhQAANtSfwm5r/r3NJwAnZeDrvvkispLHsEfT5wjV2H6\nkgmqMaL9kTjP9M16RSW1D2qa7lB6UYX294mHgjkM4yu3jgKG3sK4bixrtG3vwqna\nZAx0hvXo3dgXjACwTT6B/dH5ZltVIsDxkmDAEy+Trpkin1FTxH6tjOobMMT/2umU\nd6EcpLuwO2fFRzMxO+iGxIsXWx69C1fnFJ/jbzhPFwKBgQDtFVd5PbvHvjyhPeO0\nkxMuRX1/uONdeWzATP6bpvjg/xaz83r05N3Xezwyo0wEcI+m/NTnGSy5tRqnH9Gu\nyYfgFtN0GsFtpFMQmmdUT0Fi7wnb29BumE/dtHr1quciFpn7J1EYwqET8Zh6LHIG\nEIDOkpkrOOmkr7ZkEFiN+8yUtwKBgQDodZWfnDP+5B6uORyWTUC2fn2LqAmTfIm7\nk1rWukhk7k7+qceysHB9l8l5hw0tqT+vyNkfjCYg26QxWr6VxomgHWs1+ltpp477\nxcL+Sjzt3rUrg+S3j6WBdDfIsHhn18+DOuIaGx8D0+1T0xmDkyaUQ/2m4dB0fYTA\nCcn5ugk+8wKBgQDqKukyREpj+L9z12hAJACO9G4HqDtSSSukKKhWXy51NR9ccEHi\n/ucq/Xqw3V+pBvCsVA+JjvEBICMKLUBzLnT5XjJO/FpPDD5LKOakfH+t00JemRef\nFOvVpHzKh5oxrjc6vVO5bAujYj5jdeDNoqFG579H1LfZRQCWhxqgarNfswKBgQC3\naG7RK/FqXppGZBFGFVXFpy9vDPnhQBV7xduaKAgOemghTdueBM/8h9IS7JddO3hg\nPVvyJCXBLwrxVeOhULAtgIiiYhHV3rpVs8cR04kXNOxElR/UJKY06XlKAtI9PG0h\n4UebyWJDJubNiHVM9sEtoidzkB9kidQ/oHmXa0z4OQKBgQCSV5ZVIccZ8qS6iTYw\nPp8V1FbiX9BPI/mNw9+hiFG92GV6eYMYqV3FqYW5zaZ1lRSY3ccAXr08Jy7Vj64e\n5GeTE1UVSLVpyTPKKxdtwPH4Vf1H5l5OKjdy5/nFYWp42UcLiRKCctVliUvs9MGg\n0FLWWK2fnemCcg+JwSm0Hwn09Q==\n-----END PRIVATE KEY-----\n"));

                // Create the service.
                CalendarService service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Calendar API Sample",
                });


                //service.Events.Insert(newEvent, "nipe@hcsgops.com").Execute();

                return service;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.InnerException);
                return null;

            }
        }

        public bool InsertEvent(GoogleCalendarAppointmentModel model, CalendarService calendarservice)
        {
            bool result = false;
            try
            {
                List<EventAttendee> Attendees = new List<EventAttendee>();
                foreach (string attendeeEmail in model.CalendarEventAttendee.Split(',').ToList())
                {
                    Attendees.Add(new EventAttendee { Email = attendeeEmail });
                }

                Event newEvent = new Event()
                {
                    Summary = model.EventTitle,
                    Location = model.EventLocation,
                    Description = model.EventDetails,
                    Start = new EventDateTime()
                    {
                        DateTime = model.EventStartTime,
                        //DateTime = DateTime.Parse("2017-10-20T09:00:00-07:00"),
                        //TimeZone = "America/Los_Angeles",
                        TimeZone = "EST",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = model.EventEndTime,
                        //DateTime = DateTime.Parse("2017-10-20T17:00:00-07:00"),
                        //TimeZone = "America/Los_Angeles",
                        TimeZone = "EST",
                    },
                    //Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                    //Attendees = new EventAttendee[] {

                    //                new EventAttendee() { Email = "hiruit.1988@gmail.com" },
                    //                new EventAttendee() { Email = "30L@hcsgops.com" },
                    //                //new EventAttendee() { Email = "nipe@hcsgcorp.com" },
                    //                new EventAttendee() { Email = "mtest1@hcsgcorp.com" },
                    //                //new EventAttendee() { Email = "ipenibu@gmail.com" },
                    //            },
                    Attendees = Attendees,
                    Reminders = new Event.RemindersData()
                    {
                        UseDefault = false,
                        Overrides = new EventReminder[] {
                                    new EventReminder() { Method = "email", Minutes = 24 * 60 },
                                    //new EventReminder() { Method = "sms", Minutes = 10 },
                                }
                    }
                };
                EventsResource er = new EventsResource(calendarservice);
                //string ExpKey = "EventID";
                //string ExpVal = "1";

                //var queryEvent = er.List("primary");
                //queryEvent.SharedExtendedProperty = ExpKey + "=" + ExpVal; //"EventID=9999"
                //var EventsList = queryEvent.Execute();

                er.Insert(newEvent, "primary").Execute();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        [HttpPost]
        public ActionResult CreateEvent(CalendarEventEntity model)
        {
            GoogleCalendarAppointmentModel GoogleCalendarAppointmentModelObj = new GoogleCalendarAppointmentModel();
            GoogleCalendarAppointmentModelObj.EventID = "1";
            GoogleCalendarAppointmentModelObj.EventTitle = model.EventTitle;
            GoogleCalendarAppointmentModelObj.EventStartTime = model.EventStartTime;
            GoogleCalendarAppointmentModelObj.EventEndTime = model.EventEndTime;
            GoogleCalendarAppointmentModelObj.EventLocation = model.EventLocation;
            GoogleCalendarAppointmentModelObj.EventDetails = model.EventDetails;
            //GoogleCalendarAppointmentModelObj.CalendarEventAttendee = model.EventAttendee;
            if (model.EventAttendee != null && model.EventAttendee.Count() > 0)
            {
                GoogleCalendarAppointmentModelObj.CalendarEventAttendee = String.Join(",", model.EventAttendee.ToArray());
            }
            CalendarService calendarService = AuthenticateServiceAccount("opscalendar@opscalendar-163215.iam.gserviceaccount.com", gFolder + @"\OpsCalendar-6d9a1aae60e5.json");
            if (InsertEvent(GoogleCalendarAppointmentModelObj, calendarService))
            {
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                return View();
            }
        }

        #region Google Calendar Functions

        private readonly IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);



        private async Task<UserCredential> GetCredentialForApiAsync()
        {
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
                        {
                            ClientSecrets = new ClientSecrets
                            {
                                ClientId = AppConstants.ClientId,
                                ClientSecret = AppConstants.ClientSecret,
                            },
                            Scopes = MyRequestedScopes.Scopes,
                        };
            var flow = new GoogleAuthorizationCodeFlow(initializer);

            //var identity = await HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ApplicationCookie);
            //var identity = await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ApplicationCookie);
            var identity = await AuthenticationManager.GetExternalLoginInfoAsync();
            var userId = identity.ExternalIdentity.FindFirstValue("GoogleUserId");
            var token = await dataStore.GetAsync<TokenResponse>(userId);
            var token2 = (new List<System.Security.Claims.Claim>(identity.ExternalIdentity.Claims))[3].Value;
            return new UserCredential(flow, userId, token);
        }

        #endregion

        #region Google Authentication

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private const string XsrfKey = "XsrfId";

        [AllowAnonymous]
        public async Task<ActionResult> GoogleLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                SessionManager.IsGoogleAuthenticate = true;
                return RedirectToAction("LoginAuthentication");
            }

            if (loginInfo.Email.Contains("@"))
            {
                string[] usernamearray = loginInfo.Email.Split('@');

                if (usernamearray[1] != "hcsgops.com" && usernamearray[1] != "hcsgcorp.com")
                {
                    TempData["LoginError"] = loginInfo.Email + "is not valid Email address for access this site. Please enter valid credential";
                    return RedirectToAction("LoginAuthentication");
                    //return View();
                }
                //model.Extension = (LoginExtensions)StringEnum.Parse(typeof(LoginExtensions), usernamearray[1]);
                //SessionManager.LoginExtensions = Convert.ToString(usernamearray[1]);
            }

            SessionManager.UserName = loginInfo.DefaultUserName;
            SessionManager.UserEmail = loginInfo.Email;
            SessionManager.IsGoogleAuthenticate = true;
            return RedirectToAction("CreateEvent", "Calendar");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, SessionManager.UserEmail)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion

        #region Commented code for calendar Authentication

        /*
        public void Authenticate()
        {
            string[] scopes = new string[] {
     CalendarService.Scope.Calendar //, // Manage your calendars
    //CalendarService.Scope.CalendarReadonly // View your Calendars
 };
            string cal_user = "30l@hcsgops.com"; //your CalendarID On which you want to put events
            //you get your calender id "https://calendar.google.com/calendar"
            //go to setting >>calenders tab >> select calendar >>Under calender Detailes at Calendar Address:

            //string filePath = Server.MapPath("~/Key/key.json");
            var service = AuthenticateServiceAccount("opscalendar@opscalendar-163215.iam.gserviceaccount.com", gFolder + @"\client_secret_118074046336811904856.json", scopes);
            //"xyz@projectName.iam.gserviceaccount.com" this is your service account email id replace with your service account emailID you got it .
            //when you create service account https://console.developers.google.com/projectselector/iam-admin/serviceaccounts

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

            insert(service, cal_user, newEvent);

        }

        public static Event insert(CalendarService service, string id, Event myEvent)
        {
            try
            {
                return service.Events.Insert(myEvent, id).Execute();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static CalendarService AuthenticateServiceAccount(string serviceAccountEmail, string serviceAccountCredentialFilePath, string[] scopes)
        {
            try
            {
                if (string.IsNullOrEmpty(serviceAccountCredentialFilePath))
                    throw new Exception("Path to the service account credentials file is required.");
                if (!System.IO.File.Exists(serviceAccountCredentialFilePath))
                    throw new Exception("The service account credentials file does not exist at: " + serviceAccountCredentialFilePath);
                if (string.IsNullOrEmpty(serviceAccountEmail))
                    throw new Exception("ServiceAccountEmail is required.");

                // For Json file
                if (Path.GetExtension(serviceAccountCredentialFilePath).ToLower() == ".json")
                {
                    //GoogleCredential credential;
                    ////using(FileStream stream = File.Open(serviceAccountCredentialFilePath, FileMode.Open, FileAccess.Read, FileShare.None))


                    //using (var stream = new FileStream(serviceAccountCredentialFilePath, FileMode.Open, FileAccess.Read))
                    //{
                    //    credential = GoogleCredential.FromStream(stream)
                    //         .CreateScoped(scopes).CreateWithUser("nipe@hcsgops.com");//put a email address from which you want to send calendar its like (calendar by xyz user )
                    //}

                    //// Create the  Calendar service.
                    //return new CalendarService(new BaseClientService.Initializer()
                    //{
                    //    HttpClientInitializer = credential,
                    //    ApplicationName = "Calendar_Appointment event Using Service Account Authentication",
                    //});
                    CalendarService calService = GetService("AIzaSyDNUZnFMXagzFGq-gAwPoZsCrnybGP5HAQ");
                    return calService;
                }
                else if (Path.GetExtension(serviceAccountCredentialFilePath).ToLower() == ".p12")
                {   // If its a P12 file

                    var certificate = new X509Certificate2(serviceAccountCredentialFilePath, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
                    var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
                    {
                        Scopes = scopes
                    }.FromCertificate(certificate));

                    // Create the  Calendar service.
                    return new CalendarService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = "Calendar_Appointment event Using Service Account Authentication",

                    });
                }
                else
                {
                    throw new Exception("Something Wrong With Service accounts credentials.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Create_Service_Account_Calendar_Failed", ex);
            }
        }

        public void Test()
        {

            string[] scopes = new string[] { CalendarService.Scope.Calendar };
            GoogleCredential credential;
            using (var stream = new FileStream(gFolder + @"\client_secret_118074046336811904856.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                                .CreateScoped(scopes).CreateWithUser("opscalendar@opscalendar-163215.iam.gserviceaccount.com");
            }

            // Create the Calendar service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar Authentication Sample",
            });
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

        */
        #endregion



    }
}
