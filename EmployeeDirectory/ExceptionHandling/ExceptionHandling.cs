using System.Data.Common;
using log4net;
using log4net.Config;
using System.Net;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System;

namespace EmployeeDirectory.ExceptionHandling
{
    /// <summary>
    /// DataAccessErrorType Enum.
    /// </summary>
    enum DataAccessErrorType
    {
        /// <summary>
        /// Network Address Not Found.
        /// </summary>
        NetworkAddressNotFound,

        /// <summary>
        /// Invalid Database.
        /// </summary>
        InvalidDatabase,

        /// <summary>
        /// Login Failed.
        /// </summary>
        LoginFailed,

        /// <summary>
        /// Connection Refused.
        /// </summary>
        ConnectionRefused,

        /// <summary>
        /// SQL Server Error.
        /// </summary>
        SqlError,

        /// <summary>
        /// Failed to Connect Database.
        /// </summary>
        FailtoConnectDatabase
    }

    /// <summary>
    /// Exception Handling.
    /// </summary>
    public class ExceptionHandling
    {
        //SqlException that time this method called.
        public static int ManageErrorLog(SqlException exc)
        {
            string error = string.Empty;
            switch (exc.Number)
            {
                case (2):
                    error = Convert.ToString(DataAccessErrorType.FailtoConnectDatabase);
                    break;
                case (53):
                    error = Convert.ToString(DataAccessErrorType.NetworkAddressNotFound);
                    break;
                case (4060):
                    error = Convert.ToString(DataAccessErrorType.InvalidDatabase);
                    break;
                case (18452):
                    error = string.Empty;
                    break;
                case (18456):
                    error = Convert.ToString(DataAccessErrorType.LoginFailed);
                    break;
                case (10054):
                    error = Convert.ToString(DataAccessErrorType.ConnectionRefused);
                    break;
                case (547):
                    error = Convert.ToString(DataAccessErrorType.ConnectionRefused);
                    break;
                case (2627):
                    error = Convert.ToString(DataAccessErrorType.ConnectionRefused);
                    break;
                case (2601):
                    error = Convert.ToString(DataAccessErrorType.SqlError);
                    break;
                default:
                    error = string.Empty;
                    break;
            }

            try
            {
                ErrorHandler objErren = new ErrorHandler();
                System.Web.UI.Page p = new System.Web.UI.Page();
                string[] str;
                objErren.BranchName = string.Empty;
                objErren.CmpName = string.Empty;
                objErren.ErrDate = Convert.ToDateTime(System.DateTime.Now.ToString());
                objErren.ErrorKeyID = error;
                objErren.ErrorMessage = exc.Message.ToString();
                if (exc.InnerException != null)
                {
                    objErren.ErrorInnerMessage = exc.InnerException.ToString();
                }
                else
                {
                    objErren.ErrorInnerMessage = exc.Message.ToString();
                }

                objErren.ModuleName = HttpContext.Current.Request.Path.ToString();
                objErren.StackTrace = exc.StackTrace;
                if (HttpContext.Current.Request.QueryString.ToString() != string.Empty)
                {
                    str = HttpContext.Current.Request.QueryString.ToString().Split('=');
                }
                else
                {
                    str = HttpContext.Current.Request.Path.ToString().Split('/');
                }

                objErren.SubModuleName = str[str.Length - 1].ToString();
                objErren.PageName = p.Page.ClientQueryString;
                objErren.UserName = p.Page.User.Identity.Name.ToString();
                GenerateLogFile(objErren);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //System Excetpion that time this method called.
        public static int ManageErrorLog(Exception exc)
        {

            ErrorHandler objErren = new ErrorHandler();
            System.Web.UI.Page p = new System.Web.UI.Page();
            string[] str;
            objErren.BranchName = string.Empty;
            objErren.CmpName = string.Empty;
            objErren.ErrDate = Convert.ToDateTime(System.DateTime.Now.ToString());
            objErren.ErrorKeyID = exc.GetType().ToString();
            objErren.ErrorMessage = exc.Message.ToString();
            if (exc.InnerException != null)
            {
                objErren.ErrorInnerMessage = exc.InnerException.ToString();
            }
            else
            {
                objErren.ErrorInnerMessage = exc.Message.ToString();
            }

            objErren.ModuleName = HttpContext.Current.Request.Path.ToString();
            objErren.StackTrace = exc.StackTrace;

            if (HttpContext.Current.Request.QueryString.ToString() != string.Empty)
            {
                str = HttpContext.Current.Request.QueryString.ToString().Split('=');
            }
            else
            {
                str = HttpContext.Current.Request.Path.ToString().Split('/');
            }

            objErren.SubModuleName = str[str.Length - 1].ToString();
            objErren.PageName = p.Page.ClientQueryString;
            objErren.UserName = p.Page.User.Identity.Name.ToString();

            int id = 0;
            try
            {
                GenerateLogFile(objErren);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;
        }

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static void GenerateLogFile(ErrorHandler objErren)
        {
            string errorMessage = string.Empty;
            errorMessage += objErren.ModuleName;
            errorMessage += " || " + objErren.SubModuleName + " || " + objErren.ErrorKeyID;
            errorMessage += " -> " + objErren.ErrorMessage;
            errorMessage += "  " + objErren.StackTrace;
            log4net.Config.BasicConfigurator.Configure();
            ILog log = log4net.LogManager.GetLogger(typeof(ErrorHandler));

            System.IO.File.AppendAllText(@"C:\ApplicationLog\HCSGLog.txt", errorMessage);
            log.Error(errorMessage + "This is a error message ");
        }
    }
}