using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Helper
{
    public class DBBulkTransaction
    {
        private CommandType cmdType;

        private string cmdText;

        private DbParameter[] cmdParms;
        #region "Property"

        #region "Property:CmdType(0)"
        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>
        /// The type of the command.
        /// </value>
        public virtual CommandType CmdType
        {
            get { return this.cmdType; }
            set { this.cmdType = value; }
        }
        #endregion

        #region "Property:CmdText(0)"
        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>
        /// The command text.
        /// </value>
        public virtual string CmdText
        {
            get { return this.cmdText; }
            set { this.cmdText = value; }
        }
        #endregion

        #region "Property:CmdParms(0)"
        /// <summary>
        /// Gets or sets the command parms.
        /// </summary>
        /// <value>
        /// The command parms.
        /// </value>
        public virtual DbParameter[] CmdParms
        {
            get { return this.cmdParms; }
            set { this.cmdParms = value; }
        }
        #endregion

        #endregion
    }
}