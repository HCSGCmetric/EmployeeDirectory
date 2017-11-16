using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.ExceptionHandling
{
    public class ErrorHandler
    {
        private int mErroID;
        private string mErrorKeyID;
        private string mUserName;
        private string mCmpName;
        private string mBranchName;
        private DateTime? mErrDate;
        private string mErrorMessage;
        private string mErrorInnerMessage;
        private string mPageName;
        private string mModuleName;
        private string mSubModuleName;
        private string mStackTrace;

        public int ErroID
        {
            get { return mErroID; }
            set { mErroID = value; }
        }

        public string ErrorKeyID
        {
            get { return mErrorKeyID; }
            set { mErrorKeyID = value; }
        }

        public string UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }

        public string CmpName
        {
            get { return mCmpName; }
            set { mCmpName = value; }
        }

        public string BranchName
        {
            get { return mBranchName; }
            set { mBranchName = value; }
        }

        public DateTime? ErrDate
        {
            get { return mErrDate; }
            set { mErrDate = value; }
        }

        public string ErrorMessage
        {
            get { return mErrorMessage; }
            set { mErrorMessage = value; }
        }

        public string ErrorInnerMessage
        {
            get { return mErrorInnerMessage; }
            set { mErrorInnerMessage = value; }
        }

        public string PageName
        {
            get { return mPageName; }
            set { mPageName = value; }
        }

        public string ModuleName
        {
            get { return mModuleName; }
            set { mModuleName = value; }
        }

        public string SubModuleName
        {
            get { return mSubModuleName; }
            set { mSubModuleName = value; }
        }

        public string StackTrace
        {
            get { return mStackTrace; }
            set { mStackTrace = value; }
        }
    }
}